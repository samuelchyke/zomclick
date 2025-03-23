using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.ArtifactShop {
    public class ArtifactShopLockedPageState : ShopBaseState
    {
        private CompositeDisposable _disposables = new CompositeDisposable();
        public override void EnterState(ShopStateManager shopContext){}
        public override void ExitState(ShopStateManager shopContext){}

        public override void EnterSubState(ShopStateManager shopContext)
        {
            shopContext.artifactShopLockedPage.SetActive(true);
        }

        public override void ExitSubState(ShopStateManager shopContext)
        {
            shopContext.artifactShopLockedPage.SetActive(false);
        }
    }
}