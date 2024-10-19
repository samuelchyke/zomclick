using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.AllyShop.AllyShopPages {
    public class AllyShopPageTwoState : ShopBaseState
    {

        Button previousPageButton;
        Button nextPageButton;

        TextMeshProUGUI rodCostText;
        Button rodBuyButton;
        Button rodStatsButton;

        TextMeshProUGUI donCostText;
        Button donBuyButton;
        Button donStatsButton;

        Button allyStatsBackground;
        TextMeshProUGUI allyStatsNameText;
        TextMeshProUGUI allyStatsLevelText;
        TextMeshProUGUI allyStatsDamageText;
        TextMeshProUGUI allyStatsLoreText;

        TextMeshProUGUI level10DescriptionText;
        TextMeshProUGUI level25DescriptionText;

        Dictionary<string, TextMeshProUGUI> allyCostTexts;

        public override void EnterState(ShopStateManager shopContext)
        {
            // shopContext.allyShop.SetActive(true);
            // shopContext.allyShopPage2.SetActive(true);

            // rodCostText = GameObject.Find("rod_cost_text").GetComponent<TextMeshProUGUI>();
            // rodBuyButton = GameObject.Find("rod_buy_button").GetComponent<Button>();
            // rodStatsButton = GameObject.Find("rod_stats_button").GetComponent<Button>();

            // donCostText = GameObject.Find("don_cost_text").GetComponent<TextMeshProUGUI>();
            // donBuyButton = GameObject.Find("don_buy_button").GetComponent<Button>();
            // donStatsButton = GameObject.Find("don_stats_button").GetComponent<Button>();

            // allyCostTexts = new Dictionary<string, TextMeshProUGUI>
            // {
            //     { "rod_id", rodCostText },
            //     { "don_id", donCostText }
            // };

            // shopContext.allyShopViewModel.allies
            //     .Subscribe(allies => UpdateUI(allies));
            //     // .AddTo(_disposables);
            
            // rodBuyButton.onClick.AddListener(() => onBuy(shopContext, "doe_id"));
            // rodStatsButton.onClick.AddListener(() => showStats(shopContext, "doe_id"));

            // donBuyButton.onClick.AddListener(() => onBuy(shopContext, "john_id"));
            // donStatsButton.onClick.AddListener(() => showStats(shopContext, "john_id"));

            // previousPageButton = GameObject.Find("previous_page_button").GetComponent<Button>();
            // nextPageButton = GameObject.Find("next_page_button").GetComponent<Button>();

            // nextPageButton.onClick.AddListener(() => SwitchStates(shopContext, shopContext.allyShopPageThreeState));
            // previousPageButton.onClick.AddListener(() => SwitchStates(shopContext, shopContext.allyShopPageOneState));
        }

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
            // shopContext.allyShopPage2.SetActive(false);
            // shopContext.allyShop.SetActive(false);
        }

        public override void EnterSubState(ShopStateManager shopContext)
        {
            // shopContext.allyShop.SetActive(true);
            shopContext.allyShopPage2.SetActive(true);

            rodCostText = GameObject.Find("rod_cost_text").GetComponent<TextMeshProUGUI>();
            rodBuyButton = GameObject.Find("rod_buy_button").GetComponent<Button>();
            rodStatsButton = GameObject.Find("rod_stats_button").GetComponent<Button>();

            donCostText = GameObject.Find("don_cost_text").GetComponent<TextMeshProUGUI>();
            donBuyButton = GameObject.Find("don_buy_button").GetComponent<Button>();
            donStatsButton = GameObject.Find("don_stats_button").GetComponent<Button>();

            allyCostTexts = new Dictionary<string, TextMeshProUGUI>
            {
                { "rod_id", rodCostText },
                { "don_id", donCostText }
            };

            shopContext.allyShopViewModel.allies
                .Subscribe(allies => UpdateUI(allies));
                // .AddTo(_disposables);
            
            rodBuyButton.onClick.AddListener(() => onBuy(shopContext, "rod_id"));
            rodStatsButton.onClick.AddListener(() => showStats(shopContext, "rod_id"));

            donBuyButton.onClick.AddListener(() => onBuy(shopContext, "don_id"));
            donStatsButton.onClick.AddListener(() => showStats(shopContext, "don_id"));

            previousPageButton = GameObject.Find("previous_page_button").GetComponent<Button>();
            nextPageButton = GameObject.Find("next_page_button").GetComponent<Button>();

            nextPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.allyShopPageThreeState));
            previousPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.allyShopPageOneState));
        }

        public override void ExitSubState(ShopStateManager shopContext)
        {
            shopContext.allyShopPage2.SetActive(false);
            // shopContext.allyShop.SetActive(false);
        }
    }
}