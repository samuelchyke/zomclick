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

        Button previousPageButton;
        Button nextPageButton;
        int currentPage = 1;

        TextMeshProUGUI johnCostText;
        Button johnBuyButton;
        Button johnStatsButton;

        TextMeshProUGUI doeCostText;
        Button doeBuyButton;
        Button doeStatsButton;

        TextMeshProUGUI rodCostText;
        TextMeshProUGUI donCostText;
        TextMeshProUGUI joeCostText;
        TextMeshProUGUI doenCostText;

        Button allyStatsBackground;
        TextMeshProUGUI allyStatsNameText;
        TextMeshProUGUI allyStatsLevelText;
        TextMeshProUGUI allyStatsDamageText;
        TextMeshProUGUI allyStatsLoreText;

        TextMeshProUGUI level10DescriptionText;
        TextMeshProUGUI level25DescriptionText;

        // Dictionary<string, TextMeshProUGUI> _allyCostTexts1;
        Dictionary<string, TextMeshProUGUI> _allyCostTexts2;
        Dictionary<string, TextMeshProUGUI> _allyCostTexts3;


        Dictionary<int, Dictionary<string, TextMeshProUGUI>> pages;
        Dictionary<int, GameObject> allyPages;

        bool displayAllyPage1;
        bool displayAllyPage2;
        bool displayAllyPage3;

        public override void EnterState(ShopStateManager shopContext)
        {
            shopContext.allyShop.SetActive(true);

            // shopContext.displayAllyPage1 = true;
            SwitchSubStates(shopContext, shopContext.allyShopPageOneState);

            // previousPageButton = GameObject.Find("previous_page_button").GetComponent<Button>();
            // nextPageButton = GameObject.Find("next_page_button").GetComponent<Button>();

            // johnCostText = GameObject.Find("john_cost_text").GetComponent<TextMeshProUGUI>();
            // johnBuyButton = GameObject.Find("john_buy_button").GetComponent<Button>();
            // johnStatsButton = GameObject.Find("john_stats_button").GetComponent<Button>();

            // doeCostText = GameObject.Find("doe_cost_text").GetComponent<TextMeshProUGUI>();
            // doeBuyButton = GameObject.Find("doe_buy_button").GetComponent<Button>();
            // doeStatsButton = GameObject.Find("doe_stats_button").GetComponent<Button>();

            // allyPages = new Dictionary<int, GameObject>
            // {
            //     { 1, shopContext.allyShopPage1 },
            //     { 2, shopContext.allyShopPage2 },
            //     { 3, shopContext.allyShopPage3 },
            // };

            // // _allyCostTexts1 = new Dictionary<string, TextMeshProUGUI>
            // // {
            // //     { "john_id", johnCostText },
            // //     { "doe_id", doeCostText }
            // // };

            // _allyCostTexts2 = new Dictionary<string, TextMeshProUGUI>
            // {
            //     { "rod_id", rodCostText },
            //     { "don_id", donCostText }
            // };

            // _allyCostTexts3 = new Dictionary<string, TextMeshProUGUI>
            // {
            //     { "joe_id", joeCostText },
            //     { "doen_id", doenCostText }
            // };

            // pages = new Dictionary<int, Dictionary<string, TextMeshProUGUI>>
            // {
            //     // { 1, _allyCostTexts1 },
            //     { 2, _allyCostTexts2 },
            //     { 3, _allyCostTexts3 },
            // };

            // shopContext.allyShopViewModel.allies
            //     .Subscribe(allies => UpdateUI(allies))
            //     .AddTo(_disposables);

            // doeBuyButton.onClick.AddListener(() => onBuy(shopContext, "doe_id"));
            // doeStatsButton.onClick.AddListener(() => showStats(shopContext, "doe_id"));

            // johnBuyButton.onClick.AddListener(() => onBuy(shopContext, "john_id"));
            // johnStatsButton.onClick.AddListener(() => showStats(shopContext, "john_id"));

            // previousPageButton.onClick.AddListener(() => PreviousPage());    
            // nextPageButton.onClick.AddListener(() => nextPage());
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

        // private async void showStats (ShopStateManager shopContext, string allyId)
        // {
        //     shopContext.allyStats.SetActive(true);

        //     allyStatsBackground = GameObject.Find("ally_stats_background").GetComponent<Button>();
        //     allyStatsNameText = GameObject.Find("ally_name_text").GetComponent<TextMeshProUGUI>();
        //     allyStatsLevelText = GameObject.Find("ally_level_text").GetComponent<TextMeshProUGUI>();
        //     allyStatsDamageText = GameObject.Find("ally_damage_text").GetComponent<TextMeshProUGUI>();
        //     allyStatsLoreText = GameObject.Find("ally_lore_text").GetComponent<TextMeshProUGUI>();

        //     var ally = await shopContext.allyShopViewModel.ReadAllyStats(allyId);

        //     allyStatsNameText.text = ally.name;
        //     allyStatsLevelText.text = ally.level.ToString();
        //     allyStatsDamageText.text = ally.totalDamage.ToString();
        //     allyStatsLoreText.text = ally.lore;

        //     var allySkills = await shopContext.allyShopViewModel.ReadAllySkills(allyId);

        //     level10DescriptionText = GameObject.Find("level_10_description_text").GetComponent<TextMeshProUGUI>();
        //     level25DescriptionText = GameObject.Find("level_25_description_text").GetComponent<TextMeshProUGUI>();

        //     var _allySkillsTexts = new Dictionary<int, TextMeshProUGUI>
        //     {
        //         { 10, level10DescriptionText },
        //         { 25, level25DescriptionText }
        //     };

        //     var _allySkillsCovers = new Dictionary<int, GameObject>
        //     {
        //         { 10, GameObject.Find("level_10_cover") },
        //         { 25, GameObject.Find("level_25_cover") }
        //     };

        //     foreach (var skill in allySkills)
        //     {
        //         if (_allySkillsTexts.TryGetValue(skill.unlockLevel, out var skillText))
        //         {
        //             skillText.text = skill.description;
        //             if (skill.isUnlocked)
        //             {
        //                 GameObject.Find("level_" + skill.unlockLevel + "_cover")?.SetActive(false);
        //             }
        //         }
        //     }

        //     allyStatsBackground.onClick.AddListener(() => hideStats(shopContext));
        // }

        // private void hideStats (ShopStateManager shopContext)
        // {
        //     shopContext.allyStats.SetActive(false);
        //     allyStatsBackground.onClick.RemoveListener(() => hideStats(shopContext));
        // }

        // private void UpdateUI(List<AllyStats> allies)
        // {
        //     // johnCostText.text = allies[].unlockCost.ToString();
        //     // if (pages.TryGetValue(currentPage, out var costTexts))
        //     // {
        //     foreach (var ally in allies)
        //     {
        //         if (_allyCostTexts1.TryGetValue(ally.id, out var costText))
        //         {
        //             costText.text = ally.isUnlocked ? ally.upgradeCost.ToString() : ally.unlockCost.ToString();
        //         }
        //     }
        //     // }
        // }

        private void PreviousPage()
        {
            if (allyPages.TryGetValue(currentPage, out var prevPage))
            {
                prevPage.SetActive(false);
            }

            if (currentPage > 1)
            {
                currentPage --;
                if (allyPages.TryGetValue(currentPage, out var page))
                {
                    page.SetActive(true);
                }
            }
        }

        private void nextPage()
        {
            if (allyPages.TryGetValue(currentPage, out var prevPage))
            {
                prevPage.SetActive(false);
            }

            if (currentPage < 3)
            {
                currentPage ++;
                if (allyPages.TryGetValue(currentPage, out var page))
                {
                    page.SetActive(true);
                }
            }
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