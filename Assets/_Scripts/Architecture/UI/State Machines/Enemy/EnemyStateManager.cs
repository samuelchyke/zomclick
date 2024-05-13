using Unity.VisualScripting;
using UnityEngine;
using Zenject;
using R3;

public class EnemyStateManager : MonoBehaviour, IDamageable
{
    [Inject] EnemyStateFactory enemyStateFactory;
    [Inject] EventsManager eventsManager;
    [Inject] public IEnemyViewModel enemyViewModel;
    ReadOnlyReactiveProperty<EnemyStats> enemyStats;

    EnemyBaseState currentState;
    public EnemyWalkState walkState;
    public EnemyAttackState attackState;
    public EnemyDeadState deadState;
    public int currentHealth;

    void OnEnable() 
    {
        // eventsManager.StartListening(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_STATS_MANAGER, UpdateEnemyStats);
    }
    
    void Start()
    {
        currentHealth = enemyViewModel.enemyStats.CurrentValue.totalHealth;
        
        walkState = enemyStateFactory.CreateWalkState();
        attackState = enemyStateFactory.CreateAttackState();
        deadState = enemyStateFactory.CreateDeadState();

        currentState = walkState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentState = deadState;
            currentState.EnterState(this);
        }
    }

    void UpdateEnemyStats()
    {
        enemyStats = enemyViewModel.enemyStats; 
    }

    void OnDisable() 
    {
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_STATS_MANAGER, UpdateEnemyStats);
    }
}
