using UnityEngine;
using UnityEngine.UIElements;


public class AllyShopState : ShopBaseState
{
    public override void EnterState(ShopStateManager shopContext)
    {
        shopContext.allyShop.SetActive(true);
    }

    public override void ExitState(ShopStateManager shopContext)
    {
        shopContext.allyShop.SetActive(false);
    }
}