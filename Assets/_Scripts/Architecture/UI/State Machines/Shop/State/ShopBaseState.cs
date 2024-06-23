using System.Linq.Expressions;
using UnityEngine;
using Zenject;

public abstract class ShopBaseState : IInitializable
{
    public void Initialize(){}

    public abstract void EnterState(ShopStateManager shopContext);

    public abstract void EnterSubState(ShopStateManager shopContext);

    public abstract void ExitSubState(ShopStateManager shopContext);

    public abstract void ExitState(ShopStateManager shopContext);

    protected void SwitchStates(ShopStateManager shopContext, ShopBaseState newState){
        shopContext.currentState.ExitState(shopContext);

        shopContext.currentState = newState;

        newState.EnterState(shopContext);
    }

    protected void SwitchSubStates(ShopStateManager shopContext, ShopBaseState newState){
        shopContext.currentSubState?.ExitSubState(shopContext);

        shopContext.currentSubState = newState;

        newState.EnterSubState(shopContext);
    }
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

    public AllyShopPageOneState CreateAllyShopPageOneState()
    {
        return Create<AllyShopPageOneState>();
    }

    public AllyShopPageTwoState CreateAllyShopPageTwoState()
    {
        return Create<AllyShopPageTwoState>();
    }

    public AllyShopPageThreeState CreateAllyShopPageThreeState()
    {
        return Create<AllyShopPageThreeState>();
    }

    public ShopTabState CreateShopTabState()
    {
        return Create<ShopTabState>();
    }
}
