using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.ArtifactShop {
    public class ArtifactShopState : ShopBaseState, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        public TextMeshProUGUI artifactUnlockCostText;
        public Button artifactBuyButton;

        public override void EnterState(ShopStateManager shopContext)
        {
            shopContext.artifactShop.SetActive(true);
            shopContext.artifactShopViewModel.artifactsShopDetails.Subscribe( details => 
                shopContext.shopTabsView.currencyText.text = details.totalRelics.ToString()
            );

            shopContext.artifactShopViewModel.artifactsShopDetails.Subscribe( details => 
                shopContext.artifactShopView.artifactUnlockCostText.text = details.artifactUnlockCost.ToString()
            );

            SwitchSubStates(shopContext, shopContext.artifactShopPageLockedState);

            artifactUnlockCostText = shopContext.artifactShopView.artifactUnlockCostText;
            artifactBuyButton = shopContext.artifactShopView.artifactBuyButton;

            artifactBuyButton.onClick.AddListener(shopContext.artifactShopViewModel.UnlockArtifact);
        }

        public override void ExitState(ShopStateManager shopContext)
        {
            shopContext.artifactShopView.HideView();
            shopContext.SubscribeGold();
            Dispose();
        }

        private void UpdateUI(ArtifactShopDetails details)
        {
            artifactUnlockCostText.text = details.artifactUnlockCost.ToString();
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _disposables = new CompositeDisposable();
        }

        public override void EnterSubState(ShopStateManager shopContext){}

        public override void ExitSubState(ShopStateManager shopContext){}
    }
}