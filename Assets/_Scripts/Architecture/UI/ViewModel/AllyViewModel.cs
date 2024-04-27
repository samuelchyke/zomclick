using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using R3;

public interface IAllyViewModel
{
    ReadOnlyReactiveProperty<List<AllyStats>> allies { get; }

}

public class AllyViewModelImpl : IAllyViewModel, IInitializable
{
    readonly IReadAllyStatsUseCase readAllyStatsUseCase;
    readonly IReadAlliesStatsUseCase readAlliesStatsUseCase;
    readonly EventsManager eventsManager;

    [Inject]
    public AllyViewModelImpl(
        IReadAlliesStatsUseCase readAlliesStatsUseCase,
        IReadAllyStatsUseCase readAllyStatsUseCase,
        EventsManager eventsManager
    )
    {
        this.readAlliesStatsUseCase = readAlliesStatsUseCase;
        this.readAllyStatsUseCase = readAllyStatsUseCase;
        this.eventsManager = eventsManager;
    }

    public async void Initialize()
    {
        _allies.Value = await readAlliesStatsUseCase.Invoke(); 

        Debug.Log("Ally View Model Initialized");
        // eventsManager.StartListening(GameEvent.AllyViewModelEvent.UPDATE_ALLY_STATS, UpdateAllyStatsEvent);
        // eventsManager.StartListening(GameEvent.EnemyViewModelEvent.INFLICT_DAMAGE_ON_ALLY, TakeDamage);
        // More event subscriptions can be added as needed
    }

    ReactiveProperty<List<AllyStats>> _allies;
    public ReadOnlyReactiveProperty<List<AllyStats>> allies => _allies;

    async void PurchaseAllyStatsEvent(string allyId)
    {
        var newAllyStats = await readAllyStatsUseCase.Invoke(allyId);
        var allyStats = _allies.Value.FirstOrDefault( ally => ally.id == allyId );
        if (newAllyStats != allyStats)
        {
            int index = _allies.Value.FindIndex(ally => ally.id == allyId);
            _allies.Value[index] = newAllyStats;
        }
    }

    async void UpdateAllyStatsEvent(string allyId)
    {
        var newAllyStats = await readAllyStatsUseCase.Invoke(allyId);
        var allyStats = _allies.Value.FirstOrDefault( ally => ally.id == allyId );
        if (newAllyStats != allyStats)
        {
            int index = _allies.Value.FindIndex(ally => ally.id == allyId);
            _allies.Value[index] = newAllyStats;
        }
    }
}
