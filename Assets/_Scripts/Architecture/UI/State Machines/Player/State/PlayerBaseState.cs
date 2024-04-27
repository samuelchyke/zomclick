using System.Linq.Expressions;
using UnityEngine;
using Zenject;

public abstract class PlayerBaseState : IInitializable
{

    // GameObject projectilePrefab;
    // PlayerStateFactory playerStateFactory;
    // IPlayerViewModel playerViewModel;
    // DiContainer _container;

    // [Inject] 
    // public PlayerBaseState (
    //     GameObject projectilePrefab,
    //     PlayerStateFactory playerStateFactory,
    //     IPlayerViewModel playerViewModel,
    //     DiContainer _container
    //     )
    // {
    //     this.projectilePrefab = projectilePrefab;
    //     this.playerStateFactory = playerStateFactory;
    //     this.playerViewModel = playerViewModel;
    //     this._container = _container;
    // }



    public void Initialize()
    {
        
    }

    public abstract void EnterState(PlayerStateManager playerContext);

    public abstract void UpdateState(PlayerStateManager playerContext);

    public abstract void OnCollisionEnter2D(PlayerStateManager playerContext, Collision2D collision);

    public abstract void ExitState(PlayerStateManager playerContext);

    public abstract void CheckSwitchStates(PlayerStateManager playerContext);

    public abstract void InitaializeSubState(PlayerStateManager playerContext);

    protected void UpdateStates(){}

    protected void SwitchStates(PlayerStateManager playerContext, PlayerBaseState newState){
        ExitState(playerContext);

        playerContext.currentState = newState;

        newState.EnterState(playerContext);
    }

    protected void SetSuperState(){}

    protected void SetSubState(){}
}

public class PlayerStateFactory
{
    // readonly DiContainer container;
    // PlayerStateManager projectilePrefab;
    // GameObject projectilePrefab;
    // PlayerStateFactory playerStateFactory;
    // IPlayerViewModel playerViewModel;
    DiContainer container;

    // [Inject]
    public PlayerStateFactory(
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

    public T Create<T>() where T : PlayerBaseState
    {
        return container.Instantiate<T>();
    }

    public PlayerIdleState CreateIdleState()
    {
        return Create<PlayerIdleState>();
    }

    public PlayerAttackState CreateAttackState()
    {
        return Create<PlayerAttackState>();
    }

    public PlayerDeadState CreateDeadState()
    {
        return Create<PlayerDeadState>();
    }
}

