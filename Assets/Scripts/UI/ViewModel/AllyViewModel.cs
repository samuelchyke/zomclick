using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using R3;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Ally;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel {
    public interface IAllyViewModel
    {
        ReadOnlyReactiveProperty<AllyStats> allyStats { get; }
    }

    public class AllyViewModelImpl : IAllyViewModel, IInitializable
    {
        readonly string _allyId;
        readonly IReadAllyStatsUseCase readAllyStatsUseCase;
        readonly EventsManager eventsManager;

        [Inject]
        public AllyViewModelImpl(
            string allyId,
            IReadAllyStatsUseCase readAllyStatsUseCase,
            EventsManager eventsManager
        )
        {
            _allyId = allyId;
            this.readAllyStatsUseCase = readAllyStatsUseCase;
            this.eventsManager = eventsManager;
            // _allyId = "jhon";
        }

        ReactiveProperty<AllyStats> _allyStats = new ();
        public ReadOnlyReactiveProperty<AllyStats> allyStats => _allyStats;

        public async void Initialize()
        {
            _allyStats.Value = await readAllyStatsUseCase.Invoke(_allyId); 

            Debug.Log("Ally View Model Initialized");
            eventsManager.StartListening(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES, () => UpdateAllyStats(_allyId));
            eventsManager.TriggerEvent(GameEvent.AllyShopViewModelEvent.SHOP_VM_SETUP_COMPLETE);
            // eventsManager.StartListening(GameEvent.EnemyViewModelEvent.INFLICT_DAMAGE_ON_ALLY, TakeDamage);
            // More event subscriptions can be added as needed
        }

        async void UpdateAllyStats(string allyId)
        {
            _allyStats.Value = await readAllyStatsUseCase.Invoke(allyId);
            Debug.Log(_allyStats.Value.isUnlocked);
            Debug.Log("UpdateAllyStats triggered");
        }

        public void Cleanup()
        {
            eventsManager.StopListening(GameEvent.AllyShopViewModelEvent.UPDATE_ALLIES,() => UpdateAllyStats("john_id"));
        }
    }
}