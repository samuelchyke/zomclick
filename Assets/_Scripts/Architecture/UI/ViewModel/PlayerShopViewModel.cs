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

    readonly IReadShopDetailsUseCase readShopDetailsUseCase;    
    readonly IBuyDamageUseCase buyDamageUseCase;
    readonly IBuyCritDamageUseCase buyCritDamageUseCase; 
    readonly IBuyCritRateUseCase buyCritRateUseCase; 
    readonly IBuyHealthUseCase buyHealthUseCase; 
 
    readonly EventsManager eventsManager;

    [Inject]
    public PlayerShopViewModelImpl(
        IReadShopDetailsUseCase readShopDetailsUseCase,
        IBuyDamageUseCase buyDamageUseCase,
        IBuyCritRateUseCase buyCritRateUseCase,
        IBuyCritDamageUseCase buyCritDamageUseCase,
        IBuyHealthUseCase buyHealthUseCase,
        EventsManager eventsManager
        )
    {
        this.readShopDetailsUseCase = readShopDetailsUseCase;
        this.buyCritDamageUseCase = buyCritDamageUseCase;
        this.buyDamageUseCase = buyDamageUseCase;
        this.buyHealthUseCase = buyHealthUseCase;
        this.buyCritRateUseCase = buyCritRateUseCase;
        this.eventsManager = eventsManager;
    }

    private ReactiveProperty<PlayerShopDetails> _shopDetails = new ();
    public ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails => _shopDetails;

    public async void Initialize()
    {
        _shopDetails.Value = await readShopDetailsUseCase.Invoke();

        Debug.Log("Shop ViewModel Initialized");
        eventsManager.TriggerEvent(GameEvent.ShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.ON_DEATH, UpdateShopDetails);
        eventsManager.StartListening(GameEvent.BossViewModelEvent.ON_DEATH, UpdateShopDetails);
    }

    async void UpdateShopDetails()
    {
        _shopDetails.Value = await readShopDetailsUseCase.Invoke();
    }

    public bool SpendGold(int amount){
        if (_shopDetails.Value.totalGold >= amount)
        {
            _shopDetails.Value.totalGold -= amount;
            return true;
        }
        return false;
    }

    public async void BuyWallHealth()
    {
        if (SpendGold(_shopDetails.Value.wallHealthCost))
        {
            await buyHealthUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public async void BuyDamage()
    {
        
        if (SpendGold(_shopDetails.Value.damageCost))
        {
            await buyDamageUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public async void BuyCritRate()
    {
        if (SpendGold(_shopDetails.Value.critRateCost))
        {
            await buyCritRateUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public async void BuyCritDamage()
    {
        if (SpendGold(_shopDetails.Value.critDamageCost))
        {
            await buyCritDamageUseCase.Invoke();
            UpdateShopDetails();
        }
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }

    public void Cleanup()
    {
        eventsManager.StopListening(GameEvent.EnemyViewModelEvent.ON_DEATH, UpdateShopDetails);
        eventsManager.StopListening(GameEvent.BossViewModelEvent.ON_DEATH, UpdateShopDetails);
    }
}
