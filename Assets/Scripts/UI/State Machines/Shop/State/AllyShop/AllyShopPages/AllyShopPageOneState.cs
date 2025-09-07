using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.AllyShop.AllyShopPages {
    public class AllyShopPageOneState : ShopBaseState
    {
        AllyShopPageOneView pageView;
        AllyStatsView allyStatsView;

        Dictionary<string, TextMeshProUGUI> allyCostTexts;

        public override void EnterState(ShopStateManager shopContext){}

        private void UpdateUI(List<AllyStats> allies)
        {
            foreach (var ally in allies)
            {
                if (allyCostTexts.TryGetValue(ally.id, out var costText))
                {
                    costText.text = ally.isUnlocked ? ally.upgradeCost.ToString() : ally.unlockCost.ToString();
                }
            }
        }

        private void onBuy (ShopStateManager shopContext, string allyId)
        {
            var allyIndex = shopContext.allyShopViewModel.allies.CurrentValue.FindIndex(ally => ally.id == allyId);
            var ally = shopContext.allyShopViewModel.allies.CurrentValue[allyIndex];

            if (ally.isUnlocked){
                shopContext.allyShopViewModel.UpgradeAllyStats(allyId);
            }
            else
            {
                shopContext.allyShopViewModel.UnlockAlly(allyId);
            }
        }

        private async void showStats (ShopStateManager shopContext, string allyId)
        {
            allyStatsView.ShowStats();

            var ally = await shopContext.allyShopViewModel.ReadAllyStats(allyId);

            allyStatsView.allyStatsNameText.text = ally.name;
            allyStatsView.allyStatsLevelText.text = ally.level.ToString();
            allyStatsView.allyStatsDamageText.text = ally.totalDamage.ToString();
            allyStatsView.allyStatsLoreText.text = ally.lore;

            var allySkills = await shopContext.allyShopViewModel.ReadAllySkills(allyId);

            var allySkillsTexts = new Dictionary<int, TextMeshProUGUI>
            {
                { 10, allyStatsView.level10DescriptionText },
                { 25, allyStatsView.level25DescriptionText }
            };

            var allySkillsCovers = new Dictionary<int, GameObject>
            {
                { 10, allyStatsView.level10Cover },
                { 25, allyStatsView.level25Cover }
            };

            foreach (var skill in allySkills)
            {
                if (allySkillsTexts.TryGetValue(skill.unlockLevel, out var skillText))
                {
                    skillText.text = skill.description;
                    if (skill.isUnlocked)
                    {
                        GameObject.Find("level_" + skill.unlockLevel + "_cover")?.SetActive(false);
                    }
                }
            }

            allyStatsView.allyStatsBackground.onClick.AddListener(() => hideStats(shopContext));
        }

        private void hideStats (ShopStateManager shopContext)
        {
            allyStatsView.HideStats();
            allyStatsView.allyStatsBackground.onClick.RemoveListener(() => hideStats(shopContext));
        }

        public override void ExitState(ShopStateManager shopContext)
        {
            // shopContext.displayAllyPage1 = false;
            pageView.HidePage();
            shopContext.allyShop.SetActive(false);
        }

        public override void EnterSubState(ShopStateManager shopContext)
        {
            pageView = shopContext.allyShopPageOneView;
            allyStatsView = shopContext.allyStatsView;
            pageView.ShowPage();

            allyCostTexts = new Dictionary<string, TextMeshProUGUI>
            {
                { "john_id", pageView.johnCostText },
                { "doe_id", pageView.doeCostText }
            };

            shopContext.allyShopViewModel.allies
                .Subscribe(allies => UpdateUI(allies));

            pageView.johnBuyButton.onClick.AddListener(() => onBuy(shopContext, "john_id"));
            pageView.johnStatsButton.onClick.AddListener(() => showStats(shopContext, "john_id"));

            pageView.doeBuyButton.onClick.AddListener(() => onBuy(shopContext, "doe_id"));
            pageView.doeStatsButton.onClick.AddListener(() => showStats(shopContext, "doe_id"));

            pageView.nextPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.allyShopPageTwoState));
        }

        public override void ExitSubState(ShopStateManager shopContext)
        {
            pageView.HidePage();
        }
    }
}