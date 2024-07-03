using System;
using UnityEngine;
using Zenject;
using R3;
using System.Collections.Generic;

public interface IPlayerShopViewModel
{
    ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails { get; }
    ReadOnlyReactiveProperty<List<PlayerSkill>> playerSkills { get; }

    void UpgradePlayerStats();
    void UnlockPlayerSkill(string playerSkillId);
    void UpgradePlayerSkill(string playerSkillId);
}

public class PlayerShopViewModelImpl : IPlayerShopViewModel, IInitializable, IDisposable
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    readonly IReadShopDetailsUseCase readShopDetailsUseCase;
    readonly IReadPlayerSkillsUseCase readPlayerSkillsUseCase;
    readonly IUpgradePlayerStatsUseCase upgradePlayerStatsUseCase;
    readonly IUnlockPlayerSkillUseCase unlockPlayerSkillUseCase;
    readonly IUpgradePlayerSkillUseCase upgradePlayerSkillUseCase;
 
    readonly EventsManager eventsManager;

    [Inject]
    public PlayerShopViewModelImpl(
        IReadShopDetailsUseCase readShopDetailsUseCase,
        IReadPlayerSkillsUseCase readPlayerSkillsUseCase,
        IUpgradePlayerStatsUseCase upgradePlayerStatsUseCase,
        IUnlockPlayerSkillUseCase unlockPlayerSkillUseCase,
        IUpgradePlayerSkillUseCase upgradePlayerSkillUseCase,
        EventsManager eventsManager
        )
    {
        this.readShopDetailsUseCase = readShopDetailsUseCase;
        this.readPlayerSkillsUseCase = readPlayerSkillsUseCase;
        this.upgradePlayerStatsUseCase = upgradePlayerStatsUseCase;
        this.unlockPlayerSkillUseCase = unlockPlayerSkillUseCase;
        this.upgradePlayerSkillUseCase = upgradePlayerSkillUseCase;
        this.eventsManager = eventsManager;
    }

    private ReactiveProperty<PlayerShopDetails> _shopDetails = new ();
    public ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails => _shopDetails;

    private ReactiveProperty<List<PlayerSkill>> _playerSkills = new ();
    public ReadOnlyReactiveProperty<List<PlayerSkill>> playerSkills => _playerSkills;

    public async void Initialize()
    {
        _shopDetails.Value = await readShopDetailsUseCase.Invoke();
        _playerSkills.Value = await readPlayerSkillsUseCase.Invoke();

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

    public async void UpgradePlayerStats()
    {
        await upgradePlayerStatsUseCase.Invoke();
        UpdateShopDetails();
        eventsManager.TriggerEvent(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_STATS);
    }

    public async void UnlockPlayerSkill(string playerSkillId)
    {
        await unlockPlayerSkillUseCase.Invoke(playerSkillId);
        UpdatePlayerSkills();
        UpdateShopDetails();
    }

    public async void UpgradePlayerSkill(string playerSkillId)
    {
        await upgradePlayerSkillUseCase.Invoke(playerSkillId);
        UpdatePlayerSkills();
        UpdateShopDetails();
    }

    async void UpdatePlayerSkills()
    {
        _playerSkills.Value = await readPlayerSkillsUseCase.Invoke();
        // eventsManager.TriggerEvent(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES);
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
