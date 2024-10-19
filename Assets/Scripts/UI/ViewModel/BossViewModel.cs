using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Enemy;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Player;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;
using UnityEngine;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel {
    public interface IBossViewModel
    {
        BossStats bossStats { get; }

        void UpdateBossStats();
        void OnDeath();
        void InflictDamage();
        void IncrementBossStats();
    }

    public class BossViewModelImpl : IBossViewModel, IInitializable
    {
        IReadPlayerStatsUseCase readPlayerStatsUseCase;
        IReadBossStatsUseCase readBossStatsUseCase;
        EventsManager eventsManager;

        [Inject]
        public BossViewModelImpl(
            IReadPlayerStatsUseCase readPlayerStatsUseCase,
            IReadBossStatsUseCase readBossStatsUseCase,
            EventsManager eventsManager       
            )
        {
            this.readPlayerStatsUseCase = readPlayerStatsUseCase;
            this.readBossStatsUseCase = readBossStatsUseCase;
            this.eventsManager = eventsManager;
        }

        PlayerStats _playerStats { get; set; }
        BossStats _bossStats { get; set; }
        public BossStats bossStats => _bossStats;

        public async void Initialize()
        {
            _playerStats = await readPlayerStatsUseCase.Invoke();
            _bossStats = await readBossStatsUseCase.Invoke();

            eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStatsEvent);

            Debug.Log("Boss ViewModel Initialized");
        }

        public async void UpdateBossStats()
        {
            _bossStats = await readBossStatsUseCase.Invoke();
            eventsManager.TriggerEvent(GameEvent.BossViewModelEvent.UPDATE_BOSS_STATS);
        }

        public void InflictDamage()
        {
            eventsManager.TriggerEvent(GameEvent.BossViewModelEvent.INFLICT_DAMAGE);
        }

        public void OnDeath()
        {
            IncrementBossStats();
            eventsManager.TriggerEvent(GameEvent.BossViewModelEvent.ON_DEATH);
        }

        public void IncrementBossStats()
        {
            _bossStats.totalHealth += 100;
            _bossStats.damage += 50;
            UpdateBossStats();
        }

        async void UpdatePlayerStatsEvent()
        {
            var newPlayerStats = await readPlayerStatsUseCase.Invoke();
            if (newPlayerStats != _playerStats)
            {
                _playerStats = newPlayerStats;
            }
        }

        public void Cleanup()
        {
            eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStatsEvent);
        }
    }
}