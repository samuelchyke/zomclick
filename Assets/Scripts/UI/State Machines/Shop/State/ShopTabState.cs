using UnityEngine;
using UnityEngine.UIElements;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State {
    public class ShopTabState : ShopBaseState
    {
        public override void EnterState(ShopStateManager shopContext)
        {
            shopContext.shopTab.SetActive(true);
        }

        public override void EnterSubState(ShopStateManager shopContext)
        {
        }

        public override void ExitState(ShopStateManager shopContext)
        {
            shopContext.shopTab.SetActive(false);
        }

        public override void ExitSubState(ShopStateManager shopContext)
        {
        }
    }
}