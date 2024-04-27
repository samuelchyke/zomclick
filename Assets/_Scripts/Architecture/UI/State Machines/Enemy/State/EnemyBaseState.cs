using System.Linq.Expressions;
using UnityEngine;
using Zenject;

public abstract class EnemyBaseState : IInitializable
{
    // [Inject] protected IEnemyViewModel enemyViewModel;

    public void Initialize()
    {
        
    }

    public abstract void EnterState(EnemyStateManager enemyStateManager);

    public abstract void UpdateState(EnemyStateManager enemyStateManager);

    public abstract void OnCollisionEnter2D(EnemyStateManager enemyStateManager, Collision2D collision);
}

public class EnemyStateFactory
{
    readonly DiContainer container;

    public EnemyStateFactory(DiContainer container)
    {
        this.container = container;
    }

    public T Create<T>() where T : EnemyBaseState
    {
        return container.Instantiate<T>();
    }

    public EnemyWalkState CreateWalkState()
    {
        return Create<EnemyWalkState>();
    }

    public EnemyAttackState CreateAttackState()
    {
        return Create<EnemyAttackState>();
    }

    public EnemyDeadState CreateDeadState()
    {
        return Create<EnemyDeadState>();
    }

    public EnemyDamagedState CreateDamagedState()
    {
        return Create<EnemyDamagedState>();
    }
}

