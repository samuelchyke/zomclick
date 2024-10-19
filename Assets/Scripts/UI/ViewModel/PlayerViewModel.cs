using UnityEngine;
using Zenject;
using R3;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Player;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel {
    public interface IPlayerViewModel
    {
        ReadOnlyReactiveProperty<PlayerStats> playerStats { get; }

        void OnDeath();
    }
    public class PlayerViewModelImpl : IPlayerViewModel, IInitializable
    {
        readonly IReadPlayerStatsUseCase readPlayerStatsUseCase;
        readonly EventsManager eventsManager;

        [Inject]
        public PlayerViewModelImpl(
            IReadPlayerStatsUseCase readPlayerStatsUseCase, 
            EventsManager eventsManager
            )
        {
            this.readPlayerStatsUseCase = readPlayerStatsUseCase;
            this.eventsManager = eventsManager;
        }

        private ReactiveProperty<PlayerStats> _playerStats = new ();
        public ReadOnlyReactiveProperty<PlayerStats> playerStats => _playerStats;

        public async void Initialize()
        {
            _playerStats.Value = await readPlayerStatsUseCase.Invoke();

            Debug.Log("PLayer View Model Initialized");
            eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStatsEvent);
            eventsManager.StartListening(GameEvent.EnemyViewModelEvent.INFLICT_DAMAGE, TakeDamage);
            // eventsManager.StartListening(GameEvent.GameManagerEvent.RESTART_ROUND, Reset);
        }

        public async void UpdatePlayerStats()
        {
            // await updatePlayerStatsUseCase.Invoke(_playerStats);
            // _playerStats = await readPlayerStatsUseCase.Invoke();
        }
        
        public void TakeDamage()
        {
            // _playerStats.wallHealth -= 1;
            UpdatePlayerStats();
        }

        public void OnDeath()
        {
            eventsManager.TriggerEvent(GameEvent.GameViewModelEvent.GAME_OVER);
        }

        async void UpdatePlayerStatsEvent()
        {
            _playerStats.Value = await readPlayerStatsUseCase.Invoke();
        }
    }
}