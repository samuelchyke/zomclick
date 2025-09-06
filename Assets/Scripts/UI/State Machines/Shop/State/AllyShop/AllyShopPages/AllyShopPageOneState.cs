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
        Button nextPageButton;

        TextMeshProUGUI johnCostText;
        Button johnBuyButton;
        Button johnStatsButton;

        TextMeshProUGUI doeCostText;
        Button doeBuyButton;
        Button doeStatsButton;

        Button allyStatsBackground;
        TextMeshProUGUI allyStatsNameText;
        TextMeshProUGUI allyStatsLevelText;
        TextMeshProUGUI allyStatsDamageText;
        TextMeshProUGUI allyStatsLoreText;

        TextMeshProUGUI level10DescriptionText;
        TextMeshProUGUI level25DescriptionText;

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
            shopContext.allyStats.SetActive(true);

            allyStatsBackground = GameObject.Find("ally_stats_background").GetComponent<Button>();
            allyStatsNameText = GameObject.Find("ally_name_text").GetComponent<TextMeshProUGUI>();
            allyStatsLevelText = GameObject.Find("ally_level_text").GetComponent<TextMeshProUGUI>();
            allyStatsDamageText = GameObject.Find("ally_damage_text").GetComponent<TextMeshProUGUI>();
            allyStatsLoreText = GameObject.Find("ally_lore_text").GetComponent<TextMeshProUGUI>();

            var ally = await shopContext.allyShopViewModel.ReadAllyStats(allyId);

            allyStatsNameText.text = ally.name;
            allyStatsLevelText.text = ally.level.ToString();
            allyStatsDamageText.text = ally.totalDamage.ToString();
            allyStatsLoreText.text = ally.lore;

            var allySkills = await shopContext.allyShopViewModel.ReadAllySkills(allyId);

            level10DescriptionText = GameObject.Find("level_10_description_text").GetComponent<TextMeshProUGUI>();
            level25DescriptionText = GameObject.Find("level_25_description_text").GetComponent<TextMeshProUGUI>();

            var _allySkillsTexts = new Dictionary<int, TextMeshProUGUI>
            {
                { 10, level10DescriptionText },
                { 25, level25DescriptionText }
            };

            var _allySkillsCovers = new Dictionary<int, GameObject>
            {
                { 10, GameObject.Find("level_10_cover") },
                { 25, GameObject.Find("level_25_cover") }
            };

            foreach (var skill in allySkills)
            {
                if (_allySkillsTexts.TryGetValue(skill.unlockLevel, out var skillText))
                {
                    skillText.text = skill.description;
                    if (skill.isUnlocked)
                    {
                        GameObject.Find("level_" + skill.unlockLevel + "_cover")?.SetActive(false);
                    }
                }
            }

            allyStatsBackground.onClick.AddListener(() => hideStats(shopContext));
        }

        private void hideStats (ShopStateManager shopContext)
        {
            shopContext.allyStats.SetActive(false);
            allyStatsBackground.onClick.RemoveListener(() => hideStats(shopContext));
        }

        public override void ExitState(ShopStateManager shopContext)
        {
            // shopContext.displayAllyPage1 = false;
            shopContext.allyShopPage1.SetActive(false);
            shopContext.allyShop.SetActive(false);
        }

        public override void EnterSubState(ShopStateManager shopContext)
        {
            // shopContext.allyShop.SetActive(true);
            // shopContext.displayAllyPage1 = true;
            shopContext.allyShopPage1.SetActive(true);

            johnCostText = GameObject.Find("john_cost_text").GetComponent<TextMeshProUGUI>();
            johnBuyButton = GameObject.Find("john_buy_button").GetComponent<Button>();
            johnStatsButton = GameObject.Find("john_stats_button").GetComponent<Button>();

            doeCostText = GameObject.Find("doe_cost_text").GetComponent<TextMeshProUGUI>();
            doeBuyButton = GameObject.Find("doe_buy_button").GetComponent<Button>();
            doeStatsButton = GameObject.Find("doe_stats_button").GetComponent<Button>();

            allyCostTexts = new Dictionary<string, TextMeshProUGUI>
            {
                { "john_id", johnCostText },
                { "doe_id", doeCostText }
            };

            shopContext.allyShopViewModel.allies
                .Subscribe(allies => UpdateUI(allies));
                // .AddTo(_disposables);

            johnBuyButton.onClick.AddListener(() => onBuy(shopContext, "john_id"));
            johnStatsButton.onClick.AddListener(() => showStats(shopContext, "john_id"));

            doeBuyButton.onClick.AddListener(() => onBuy(shopContext, "doe_id"));
            doeStatsButton.onClick.AddListener(() => showStats(shopContext, "doe_id"));

            nextPageButton = GameObject.Find("next_page_button").GetComponent<Button>();
            nextPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.allyShopPageTwoState));
        }

        public override void ExitSubState(ShopStateManager shopContext)
        {
            shopContext.allyShopPage1.SetActive(false);
            // shopContext.allyShop.SetActive(false);
        }
    }
}