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
            SwitchSubStates(shopContext, shopContext.artifactShopPageLockedState);

            artifactUnlockCostText = GameObject.Find("artifact_unlock_cost_text").GetComponent<TextMeshProUGUI>();
            artifactBuyButton = GameObject.Find("artifact_buy_button").GetComponent<Button>();
            
            shopContext.artifactShopViewModel.artifactsShopDetails
                .Subscribe(details => UpdateUI(details))
                .AddTo(_disposables);

            artifactBuyButton.onClick.AddListener(shopContext.artifactShopViewModel.UnlockArtifact);
        }

        public override void ExitState(ShopStateManager shopContext)
        {
            shopContext.artifactShop.SetActive(false);
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