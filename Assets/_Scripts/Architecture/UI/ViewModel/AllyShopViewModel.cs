using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using R3;

public interface IAllyShopViewModel
{
    ReadOnlyReactiveProperty<List<AllyStats>> allies { get; }

    void UnlockAlly(string allyId);
    void UpgradeAllyStats(string allyId);
}

public class AllyShopViewModelImpl : IAllyShopViewModel, IInitializable
{
    readonly IReadAllyStatsUseCase readAllyStatsUseCase;
    readonly IReadAlliesStatsUseCase readAlliesStatsUseCase;
    readonly IUnlockAllyUseCase unlockAllyUseCase;
    readonly IUpgradeAllyStatsUseCase upgradeAllyStatsUseCase;
    readonly EventsManager eventsManager;

    [Inject]
    public AllyShopViewModelImpl(
        IReadAlliesStatsUseCase readAlliesStatsUseCase,
        IReadAllyStatsUseCase readAllyStatsUseCase,
        IUnlockAllyUseCase unlockAllyUseCase,
        IUpgradeAllyStatsUseCase upgradeAllyStatsUseCase,
        EventsManager eventsManager
    )
    {
        this.readAlliesStatsUseCase = readAlliesStatsUseCase;
        this.readAllyStatsUseCase = readAllyStatsUseCase;
        this.unlockAllyUseCase = unlockAllyUseCase;
        this.upgradeAllyStatsUseCase = upgradeAllyStatsUseCase;
        this.eventsManager = eventsManager;
    }

    ReactiveProperty<List<AllyStats>> _allies = new ();
    public ReadOnlyReactiveProperty<List<AllyStats>> allies => _allies;

    public async void Initialize()
    {
        _allies.Value = await readAlliesStatsUseCase.Invoke(); 

        Debug.Log("Ally Shop View Model Initialized");
        eventsManager.TriggerEvent(GameEvent.AllyShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);

            // eventsManager.StartListening(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES, () => UpdateAllyStats(_allyId));
        // eventsManager.StartListening(GameEvent.AllyViewModelEvent.UPDATE_ALLY_STATS, UpdateAllyStatsEvent);
        // eventsManager.StartListening(GameEvent.EnemyViewModelEvent.INFLICT_DAMAGE_ON_ALLY, TakeDamage);
        // More event subscriptions can be added as needed
    }

    public async void UnlockAlly(string allyId)
    {
        await unlockAllyUseCase.Invoke(allyId);
        UpdateAllies();
    }

    public async void UpgradeAllyStats(string allyId)
    {
        await upgradeAllyStatsUseCase.Invoke(allyId);
        // UpdateAllyStats(allyId);
        UpdateAllies();

    }

    async void UpdateAllies()
    {
        _allies.Value = await readAlliesStatsUseCase.Invoke();
        eventsManager.TriggerEvent(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES);
    }

    async void UpdateAllyStats(string allyId)
    {
        var allyIndex = _allies.Value.FindIndex(ally => ally.id == allyId);
        _allies.Value[allyIndex] = await readAllyStatsUseCase.Invoke(allyId);
    }
}
