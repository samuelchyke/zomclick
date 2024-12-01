using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.PlayerShop.PlayerShopPages {
    public class PlayerShopPageOneState : ShopBaseState
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        Button nextPageButton;

        public TextMeshProUGUI bigBettyCostText;
        public TextMeshProUGUI turretCostText;
        public TextMeshProUGUI lightningRoundsCostText;

        public Button bigBettyBuyButton;
        public Button turretBuyButton;
        public Button lightningRoundsBuyButton;

        Dictionary<string, TextMeshProUGUI> skillsCostTexts;

        public override void EnterState(ShopStateManager shopContext){}

        private void onBuy (ShopStateManager shopContext, PlayerSkill playerSkill)
        {

            Debug.Log("onBuy: " + playerSkill.id);
            Debug.Log("onBuy: " + playerSkill.isUnlocked);

            if (playerSkill.isUnlocked){
                shopContext.playerShopViewModel.UpgradePlayerSkill(playerSkill.id);
            }
            else
            {
                shopContext.playerShopViewModel.UnlockPlayerSkill(playerSkill.id);
            }
        }

        public override void ExitState(ShopStateManager shopContext){}

        public override void EnterSubState(ShopStateManager shopContext)
        {
            shopContext.playerShopPage1.SetActive(true);

            SetUI();

            var playerSkills = shopContext.playerShopViewModel.playerSkills.CurrentValue;

            skillsCostTexts = new Dictionary<string, TextMeshProUGUI>
            {
                { playerSkills.bigBetty.id, bigBettyCostText },
                { playerSkills.turret.id, turretCostText },
                { playerSkills.lightningRounds.id, lightningRoundsCostText }
            };

            shopContext.playerShopViewModel.playerSkills.Subscribe(skills => 
                {
                    playerSkills = skills;
                    UpdateSkillsUI(skills);
                }
            )
            .AddTo(_disposables);

            bigBettyBuyButton.onClick.AddListener(() => onBuy(shopContext, playerSkills.bigBetty));
            turretBuyButton.onClick.AddListener(() => onBuy(shopContext, playerSkills.turret));
            lightningRoundsBuyButton.onClick.AddListener(() => onBuy(shopContext, playerSkills.lightningRounds));

            nextPageButton = GameObject.Find("next_page_button").GetComponent<Button>();
            nextPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.playerShopPageTwoState));
        }

        private void UpdateSkillsUI(PlayerSkills playerSkills)
        {
            UpdateSkillUI(playerSkills.bigBetty);
            UpdateSkillUI(playerSkills.turret);
            UpdateSkillUI(playerSkills.lightningRounds);
        }

        private void UpdateSkillUI(PlayerSkill skill)
        {
            if (skillsCostTexts.TryGetValue(skill.id, out var costText))
            {
                costText.text = skill.isUnlocked ? skill.upgradeCost.ToString() : skill.unlockCost.ToString();
            }
        }

        private void SetUI()
        {
            bigBettyCostText = GameObject.Find("big_betty_gold_text").GetComponent<TextMeshProUGUI>();
            turretCostText = GameObject.Find("turret_gold_text").GetComponent<TextMeshProUGUI>();
            lightningRoundsCostText = GameObject.Find("crit_rate_gold_text").GetComponent<TextMeshProUGUI>();

            bigBettyBuyButton = GameObject.Find("big_betty_buy_button").GetComponent<Button>();
            turretBuyButton = GameObject.Find("turret_buy_button").GetComponent<Button>();
            lightningRoundsBuyButton = GameObject.Find("crit_rate_buy_button").GetComponent<Button>();
        }

        public override void ExitSubState(ShopStateManager shopContext)
        {
            shopContext.playerShopPage1.SetActive(false);
        }
    }
}