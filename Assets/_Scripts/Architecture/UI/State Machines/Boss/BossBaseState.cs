using System.Linq.Expressions;
using UnityEngine;
using Zenject;

public abstract class BossBaseState : IInitializable
{
    public void Initialize()
    {
        
    }

    public abstract void EnterState(BossStateManager bossContext);

    public abstract void UpdateState(BossStateManager bossContext);

    public abstract void OnCollisionEnter2D(BossStateManager bossContext, Collision2D collision);

    public abstract void ExitState(BossStateManager bossContext);

    public abstract void CheckSwitchStates(BossStateManager bossContext);

    public abstract void InitaializeSubState(BossStateManager bossContext);

    protected void UpdateStates(){}

    protected void SwitchStates(BossStateManager bossContext, BossBaseState newState){
        ExitState(bossContext);

        bossContext.SwitchState(newState);

        newState.EnterState(bossContext);
    }

    protected void SetSuperState(){}

    protected void SetSubState(){}
}

public class BossStateFactory
{
    // readonly DiContainer container;
    // PlayerStateManager projectilePrefab;
    // GameObject projectilePrefab;
    // PlayerStateFactory playerStateFactory;
    // IPlayerViewModel playerViewModel;
    DiContainer container;

    // [Inject]
    public BossStateFactory(
        // GameObject projectilePrefab,
        // PlayerStateFactory playerStateFactory,
        // IPlayerViewModel playerViewModel,
        DiContainer container
        )
    {
        // this.projectilePrefab = projectilePrefab;
        // this.playerStateFactory = playerStateFactory;
        // this.playerViewModel = playerViewModel;
        this.container = container;
    }

    public T Create<T>() where T : BossBaseState
    {
        return container.Instantiate<T>();
    }

    public BossWalkState CreateWalkState()
    {
        return Create<BossWalkState>();
    }

    public BossAttackState CreateAttackState()
    {
        return Create<BossAttackState>();
    }

    public BossDeadState CreateDeadState()
    {
        return Create<BossDeadState>();
    }
}

