using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.AllyShop {
    public class AllyShopState : ShopBaseState, IDisposable
    {
        CompositeDisposable _disposables = new CompositeDisposable();

        public override void EnterState(ShopStateManager shopContext)
        {
            shopContext.allyShop.SetActive(true);
            SwitchSubStates(shopContext, shopContext.allyShopPageOneState);
        }

        public override void ExitState(ShopStateManager shopContext)
        {
            shopContext.allyShop.SetActive(false);
            Dispose();
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _disposables = new CompositeDisposable();
        }

        public override void EnterSubState(ShopStateManager shopContext)
        {}

        public override void ExitSubState(ShopStateManager shopContext)
        {}
    }
}