using System;
using UnityEngine;
using Zenject;
using R3;

public interface IPlayerUpgradeShopViewModel
{
    ReadOnlyReactiveProperty<PlayerUpgradeShopDetails> shopDetails { get; }
    
    void BuyWallHealth();
    void BuyDamage();
    void BuyCritRate();
    void BuyCritDamage();
}

public class PlayerUpgradeShopViewModelImpl : IPlayerUpgradeShopViewModel, IInitializable, IDisposable
{
    private CompositeDisposable _disposables = new CompositeDisposable();
    readonly IReadEnemyStatsUseCase readEnemyStatsUseCase;
    readonly IReadBossStatsUseCase readBossStatsUseCase;
    readonly IReadPlayerStatsUseCase readPlayerStatsUseCase;
    readonly IUpdatePlayerStatsUseCase updatePlayerStatsUseCase;
    readonly IUpdateShopDetailsUseCase updateShopDetailsUseCase;
    readonly IReadShopDetailsUseCase readShopDetailsUseCaseUseCase;    
    readonly EventsManager eventsManager;

    [Inject]
    public PlayerUpgradeShopViewModelImpl(
        IReadEnemyStatsUseCase readEnemyStatsUseCase,
        IReadBossStatsUseCase readBossStatsUseCase,
        IReadPlayerStatsUseCase readPlayerStatsUseCase,
        IUpdatePlayerStatsUseCase updatePlayerStatsUseCase,
        IUpdateShopDetailsUseCase updateShopDetailsUseCase,
        IReadShopDetailsUseCase readShopDetailsUseCaseUseCase,
        EventsManager eventsManager
        )
    {
        this.readEnemyStatsUseCase = readEnemyStatsUseCase;
        this.readBossStatsUseCase = readBossStatsUseCase;
        this.readPlayerStatsUseCase = readPlayerStatsUseCase;
        this.updatePlayerStatsUseCase = updatePlayerStatsUseCase;
        this.updateShopDetailsUseCase = updateShopDetailsUseCase;
        this.readShopDetailsUseCaseUseCase = readShopDetailsUseCaseUseCase;
        this.eventsManager = eventsManager;
    }

    EnemyStats _enemyStats { get; set; }
    BossStats _bossStats { get; set; }
    PlayerStats _playerStats { get; set; }

    private ReactiveProperty<PlayerUpgradeShopDetails> _shopDetails = new ReactiveProperty<PlayerUpgradeShopDetails>();
    public ReadOnlyReactiveProperty<PlayerUpgradeShopDetails> shopDetails => _shopDetails;

    public async void Initialize()
    {
        _shopDetails.Value = await readShopDetailsUseCaseUseCase.Invoke();
        _playerStats = await readPlayerStatsUseCase.Invoke();
        _enemyStats = await readEnemyStatsUseCase.Invoke();
        _bossStats = await readBossStatsUseCase.Invoke();

        Debug.Log("Shop ViewModel Initialized");
        eventsManager.TriggerEvent(GameEvent.ShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.ON_DEATH, AddEnemyGold);
        eventsManager.StartListening(GameEvent.BossViewModelEvent.ON_DEATH, AddBossGold);
    }

    async void UpdatePlayerStats()
    {
        await updatePlayerStatsUseCase.Invoke(_playerStats);
        eventsManager.TriggerEvent(GameEvent.ShopViewModelEvent.UPDATE_PLAYER_STATS); 
    }

    async void UpdateShopDetails()
    {
        await updateShopDetailsUseCase.Invoke(_shopDetails.Value);
        _shopDetails.ForceNotify();
    }

    public void AddEnemyGold()
    {
        // _shopDetails.Value.totalGold += _enemyStats.goldDropAmount;
        UpdateShopDetails();
    }

    public void AddBossGold()
    {
        // _shopDetails.Value.totalGold += _enemyStats.goldDropAmount;
        UpdateShopDetails();
    }

    public bool SpendGold(int amount){
        if (_playerStats.totalGold >= amount)
        {
            _playerStats.totalGold -= amount;
            return true;
        }
        return false;
    }

    public void BuyWallHealth()
    {
        if (SpendGold(_shopDetails.Value.wallHealthCost))
        {
            _playerStats.wallHealth += 10;
            _shopDetails.Value.wallHealthCost += 10;
            UpdatePlayerStats();
            UpdateShopDetails();
        }
    }

    public void BuyDamage()
    {
        
        if (SpendGold(_shopDetails.Value.damageCost))
        {
            _playerStats.baseDamage += 10;
            _shopDetails.Value.damageCost += 10;            
            UpdatePlayerStats();
            UpdateShopDetails();
        }
    }

    public void BuyCritRate()
    {
        if (SpendGold(_shopDetails.Value.critRateCost))
        {
            _playerStats.critRate += 1;
            _shopDetails.Value.critRateCost += 10;
            UpdatePlayerStats();
            UpdateShopDetails();
        }
    }

    public void BuyCritDamage()
    {
        if (SpendGold(_shopDetails.Value.critDamageCost))
        {
            _playerStats.critMultiplier += 1;
            _shopDetails.Value.critDamageCost += 10;
            UpdatePlayerStats();
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
