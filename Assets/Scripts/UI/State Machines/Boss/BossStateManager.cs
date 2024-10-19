using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Boss.State;
using Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel;
using UnityEngine;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Boss {
    public class BossStateManager : MonoBehaviour, IDamageable
    {
        [Inject] BossStateFactory bossStateFactory;
        [Inject] EventsManager eventsManager;
        [Inject] public IBossViewModel bossViewModel;
        BossStats bossStats;

        private BossBaseState _currentState;
        public BossBaseState currentState { get => _currentState; }

        public BossWalkState walkState;
        public BossAttackState attackState;
        public BossDeadState deadState;
        public int currentHealth;

        void OnEnable() 
        {
            eventsManager.StartListening(GameEvent.BossViewModelEvent.UPDATE_BOSS_STATS, UpdateBossStats);
        }

        void Start()
        {
            bossStats = bossViewModel.bossStats;
            currentHealth = bossStats.totalHealth;

            walkState = bossStateFactory.CreateWalkState();
            attackState = bossStateFactory.CreateAttackState();
            deadState = bossStateFactory.CreateDeadState();

            _currentState = walkState;

            _currentState.EnterState(this);
        }

        void Update()
        {
            currentState.UpdateState(this);
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            currentState.OnCollisionEnter2D(this, collision);
        }

        public void SwitchState(BossBaseState state)
        {
            _currentState = state;
            _currentState.EnterState(this);
        }

        public void TakeDamage(int damage)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                SwitchState(deadState);
            }
        }

        void UpdateBossStats()
        {
            bossStats = bossViewModel.bossStats;
        }

        void OnDisable() 
        {
            eventsManager.StopListening(GameEvent.BossViewModelEvent.UPDATE_BOSS_STATS, UpdateBossStats);
        }
    }
}