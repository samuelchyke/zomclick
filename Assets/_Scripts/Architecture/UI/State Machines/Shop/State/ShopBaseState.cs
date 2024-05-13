using System.Linq.Expressions;
using UnityEngine;
using Zenject;

public abstract class ShopBaseState : IInitializable
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

    public abstract void EnterState(ShopStateManager shopContext);

    public abstract void ExitState(ShopStateManager shopContext);

    protected void SwitchStates(ShopStateManager shopContext, ShopBaseState newState){
        ExitState(shopContext);

        shopContext.currentState = newState;

        newState.EnterState(shopContext);
    }

    protected void SetSuperState(){}

    protected void SetSubState(){}
}

public class ShopStateFactory
{
    // readonly DiContainer container;
    // PlayerStateManager projectilePrefab;
    // GameObject projectilePrefab;
    // PlayerStateFactory playerStateFactory;
    // IPlayerViewModel playerViewModel;
    DiContainer container;

    // [Inject]
    public ShopStateFactory(
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

    public T Create<T>() where T : ShopBaseState
    {
        return container.Instantiate<T>();
    }

    public PlayerShopState CreatePlayerShopState()
    {
        return Create<PlayerShopState>();
    }

    public AllyShopState CreateAllyShopState()
    {
        return Create<AllyShopState>();
    }

    public ShopTabState CreateShopTabState()
    {
        return Create<ShopTabState>();
    }
}

