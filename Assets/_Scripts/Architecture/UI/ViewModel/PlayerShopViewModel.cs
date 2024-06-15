using System;
using UnityEngine;
using Zenject;
using R3;

public interface IPlayerShopViewModel
{
    ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails { get; }

    void UpgradePlayerStats();
    void UnlockPlayerSkill(string playerSkillId);
    void UpgradePlayerSkill(string playerSkillId);
}

public class PlayerShopViewModelImpl : IPlayerShopViewModel, IInitializable, IDisposable
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    readonly IReadShopDetailsUseCase readShopDetailsUseCase;
    readonly IUpgradePlayerStatsUseCase upgradePlayerStatsUseCase;
    readonly IUnlockPlayerSkillUseCase unlockPlayerSkillUseCase;
    readonly IUpgradePlayerSkillUseCase upgradePlayerSkillUseCase;
 
    readonly EventsManager eventsManager;

    [Inject]
    public PlayerShopViewModelImpl(
        IReadShopDetailsUseCase readShopDetailsUseCase,
        IUpgradePlayerStatsUseCase upgradePlayerStatsUseCase,
        IUnlockPlayerSkillUseCase unlockPlayerSkillUseCase,
        IUpgradePlayerSkillUseCase upgradePlayerSkillUseCase,
        EventsManager eventsManager
        )
    {
        this.readShopDetailsUseCase = readShopDetailsUseCase;
        this.upgradePlayerStatsUseCase = upgradePlayerStatsUseCase;
        this.unlockPlayerSkillUseCase = unlockPlayerSkillUseCase;
        this.upgradePlayerSkillUseCase = upgradePlayerSkillUseCase;
        this.eventsManager = eventsManager;
    }

    private ReactiveProperty<PlayerShopDetails> _shopDetails = new ();
    public ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails => _shopDetails;

    public async void Initialize()
    {
        _shopDetails.Value = await readShopDetailsUseCase.Invoke();

        Debug.Log("Shop ViewModel Initialized");
        eventsManager.TriggerEvent(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.ON_DEATH, UpdateShopDetails);
        eventsManager.StartListening(GameEvent.BossViewModelEvent.ON_DEATH, UpdateShopDetails);
        eventsManager.StartListening(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES, UpdateShopDetails);
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

    public void UpgradePlayerStats()
    {
        upgradePlayerStatsUseCase.Invoke();
        UpdateShopDetails();
    }

    public async void UnlockPlayerSkill(string playerSkillId)
    {
        await unlockPlayerSkillUseCase.Invoke(playerSkillId);
        UpdateShopDetails();
    }

    public async void UpgradePlayerSkill(string playerSkillId)
    {
        await upgradePlayerSkillUseCase.Invoke(playerSkillId);
        UpdateShopDetails();
    }

    public void Dispose()
    {
        _disposables.Dispose();
    }

    public void Cleanup()
    {
        eventsManager.StopListening(GameEvent.EnemyViewModelEvent.ON_DEATH, UpdateShopDetails);
        eventsManager.StopListening(GameEvent.BossViewModelEvent.ON_DEATH, UpdateShopDetails);
        eventsManager.StopListening(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES, UpdateShopDetails);
    }
}
