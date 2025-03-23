using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.ArtifactShop {
    public class ArtifactShopUnlockedPageState : ShopBaseState
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        public TextMeshProUGUI artifactCostText;
        public Button artifactUnlockButton;

        public override void EnterState(ShopStateManager shopContext){}

        private void onUnlockArtifact (ShopStateManager shopContext)
        {
            shopContext.artifactShopViewModel.UnlockArtifact();
        }

        public override void ExitState(ShopStateManager shopContext){}

        public override void EnterSubState(ShopStateManager shopContext)
        {
            shopContext.artifactShopUnlockedPage.SetActive(true);

            SetUI();

            artifactUnlockButton.onClick.AddListener(() => onUnlockArtifact(shopContext));
        }

        private void SetUI()
        {
            artifactCostText = GameObject.Find("artifact_cost_text").GetComponent<TextMeshProUGUI>();
            artifactUnlockButton = GameObject.Find("artifact_unlock_button").GetComponent<Button>();
        }

        public override void ExitSubState(ShopStateManager shopContext)
        {
            shopContext.artifactShopUnlockedPage.SetActive(false);
        }
    }
}