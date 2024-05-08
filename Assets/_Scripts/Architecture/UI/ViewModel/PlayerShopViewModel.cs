using System;
using UnityEngine;
using Zenject;
using R3;

public interface IPlayerShopViewModel
{
    ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails { get; }
    
    void BuyWallHealth();
    void BuyDamage();
    void BuyCritRate();
    void BuyCritDamage();
}

public class PlayerShopViewModelImpl : IPlayerShopViewModel, IInitializable, IDisposable
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    readonly IUpdateShopDetailsUseCase updateShopDetailsUseCase;
    readonly IReadShopDetailsUseCase readShopDetailsUseCase;    
    readonly IAddEnemyGoldUseCase addEnemyGoldUseCase;   
    readonly IBuyDamageUseCase buyDamageUseCase;
    readonly IBuyCritDamageUseCase buyCritDamageUseCase; 
    readonly IBuyCritRateUseCase buyCritRateUseCase; 
    readonly IBuyHealthUseCase buyHealthUseCase; 
 
    readonly EventsManager eventsManager;

    [Inject]
    public PlayerShopViewModelImpl(
        IUpdateShopDetailsUseCase updateShopDetailsUseCase,
        IReadShopDetailsUseCase readShopDetailsUseCase,
        IAddEnemyGoldUseCase addEnemyGoldUseCase,
        IBuyDamageUseCase buyDamageUseCase,
        IBuyCritRateUseCase buyCritRateUseCase,
        IBuyCritDamageUseCase buyCritDamageUseCase,
        IBuyHealthUseCase buyHealthUseCase,
        EventsManager eventsManager
        )
    {
        this.updateShopDetailsUseCase = updateShopDetailsUseCase;
        this.readShopDetailsUseCase = readShopDetailsUseCase;
        this.addEnemyGoldUseCase = addEnemyGoldUseCase;
        this.buyCritDamageUseCase = buyCritDamageUseCase;
        this.buyDamageUseCase = buyDamageUseCase;
        this.buyHealthUseCase = buyHealthUseCase;
        this.buyCritRateUseCase = buyCritRateUseCase;
        this.eventsManager = eventsManager;
    }

    private ReactiveProperty<PlayerShopDetails> _shopDetails = new ReactiveProperty<PlayerShopDetails>();
    public ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails => _shopDetails;

    public async void Initialize()
    {
        _shopDetails.Value = await readShopDetailsUseCase.Invoke();

        Debug.Log("Shop ViewModel Initialized");
        eventsManager.TriggerEvent(GameEvent.ShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.ON_DEATH, AddEnemyGold);
        eventsManager.StartListening(GameEvent.BossViewModelEvent.ON_DEATH, AddBossGold);
    }

    async void UpdateShopDetails()
    {
        // await updateShopDetailsUseCase.Invoke(_shopDetails.Value);
        _shopDetails.Value = await readShopDetailsUseCase.Invoke();
        // _shopDetails.ForceNotify();
    }

    async void AddEnemyGold()
    {
        // _playerStats.totalGold += _enemyStats.goldDropAmount;
        // UpdatePlayerStats();
        await addEnemyGoldUseCase.Invoke();   
        _shopDetails.Value = await readShopDetailsUseCase.Invoke();

        // UpdateShopDetails();
    }

    public void AddBossGold()
    {
    //     _playerStats.totalGold += _enemyStats.goldDropAmount;
    //     UpdatePlayerStats();
    // }
    }

    public bool SpendGold(int amount){
        if (_shopDetails.Value.totalGold >= amount)
        {
            _shopDetails.Value.totalGold -= amount;
            return true;
        }
        return false;
    }

    public void BuyWallHealth()
    {
        if (SpendGold(_shopDetails.Value.wallHealthCost))
        {
            buyHealthUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public void BuyDamage()
    {
        
        if (SpendGold(_shopDetails.Value.damageCost))
        {
            buyDamageUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public void BuyCritRate()
    {
        if (SpendGold(_shopDetails.Value.critRateCost))
        {
            buyCritRateUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public void BuyCritDamage()
    {
        if (SpendGold(_shopDetails.Value.critDamageCost))
        {
            buyCritDamageUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }

    public void Cleanup()
    {
        eventsManager.StopListening(GameEvent.EnemyViewModelEvent.ON_DEATH, AddEnemyGold);
        eventsManager.StopListening(GameEvent.BossViewModelEvent.ON_DEATH, AddBossGold);
    }
}
