using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.PlayerShop {
    public class PlayerShopState : ShopBaseState, IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        public TextMeshProUGUI playerUpgradeCostText;
        public Button playerUpgradeBuyButton;

        public override void EnterState(ShopStateManager shopContext)
        {
            shopContext.playerUpgradeShop.SetActive(true);
            SwitchSubStates(shopContext, shopContext.playerShopPageOneState);

            playerUpgradeCostText = GameObject.Find("player_upgrade_gold_text").GetComponent<TextMeshProUGUI>();

            playerUpgradeBuyButton = GameObject.Find("player_upgrade_buy_button").GetComponent<Button>();
            
            shopContext.playerShopViewModel.shopDetails
                .Subscribe(details => UpdateUI(details))
                .AddTo(_disposables);

            playerUpgradeBuyButton.onClick.AddListener(shopContext.playerShopViewModel.UpgradePlayerStats);
        }

        public override void ExitState(ShopStateManager shopContext)
        {
            shopContext.playerUpgradeShop.SetActive(false);
            Dispose();
        }

        private void UpdateUI(PlayerShopDetails details)
        {
            playerUpgradeCostText.text = details.damageCost.ToString();
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