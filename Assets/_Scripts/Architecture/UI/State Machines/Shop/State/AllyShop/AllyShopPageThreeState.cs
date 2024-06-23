using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;

public class AllyShopPageThreeState : ShopBaseState
{
    Button previousPageButton;

    TextMeshProUGUI joeCostText;
    Button joeBuyButton;
    Button joeStatsButton;

    TextMeshProUGUI doenCostText;
    Button doenBuyButton;
    Button doenStatsButton;

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
        shopContext.allyShop.SetActive(true);
        shopContext.allyShopPage3.SetActive(true);

        joeCostText = GameObject.Find("joe_cost_text").GetComponent<TextMeshProUGUI>();
        joeBuyButton = GameObject.Find("joe_buy_button").GetComponent<Button>();
        joeStatsButton = GameObject.Find("joe_stats_button").GetComponent<Button>();

        doenCostText = GameObject.Find("doen_cost_text").GetComponent<TextMeshProUGUI>();
        doenBuyButton = GameObject.Find("doen_buy_button").GetComponent<Button>();
        doenStatsButton = GameObject.Find("doen_stats_button").GetComponent<Button>();

        allyCostTexts = new Dictionary<string, TextMeshProUGUI>
        {
            { "joe_id", joeCostText },
            { "doen_id", doenCostText }
        };

        shopContext.allyShopViewModel.allies
            .Subscribe(allies => UpdateUI(allies));
            // .AddTo(_disposables);
        
        joeBuyButton.onClick.AddListener(() => onBuy(shopContext, "joe_id"));
        joeStatsButton.onClick.AddListener(() => showStats(shopContext, "joe_id"));

        doenBuyButton.onClick.AddListener(() => onBuy(shopContext, "doen_id"));
        doenStatsButton.onClick.AddListener(() => showStats(shopContext, "doen_id"));

        previousPageButton = GameObject.Find("previous_page_button").GetComponent<Button>();
        previousPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.allyShopPageTwoState));
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
        shopContext.allyShopPage3.SetActive(false);
        shopContext.allyShop.SetActive(false);
    }

    public override void EnterSubState(ShopStateManager shopContext)
    {
        // shopContext.allyShop.SetActive(true);
        shopContext.allyShopPage3.SetActive(true);

        joeCostText = GameObject.Find("joe_cost_text").GetComponent<TextMeshProUGUI>();
        joeBuyButton = GameObject.Find("joe_buy_button").GetComponent<Button>();
        joeStatsButton = GameObject.Find("joe_stats_button").GetComponent<Button>();

        doenCostText = GameObject.Find("doen_cost_text").GetComponent<TextMeshProUGUI>();
        doenBuyButton = GameObject.Find("doen_buy_button").GetComponent<Button>();
        doenStatsButton = GameObject.Find("doen_stats_button").GetComponent<Button>();

        allyCostTexts = new Dictionary<string, TextMeshProUGUI>
        {
            { "joe_id", joeCostText },
            { "doen_id", doenCostText }
        };

        shopContext.allyShopViewModel.allies
            .Subscribe(allies => UpdateUI(allies));
            // .AddTo(_disposables);
        
        joeBuyButton.onClick.AddListener(() => onBuy(shopContext, "joe_id"));
        joeStatsButton.onClick.AddListener(() => showStats(shopContext, "joe_id"));

        doenBuyButton.onClick.AddListener(() => onBuy(shopContext, "doen_id"));
        doenStatsButton.onClick.AddListener(() => showStats(shopContext, "doen_id"));

        previousPageButton = GameObject.Find("previous_page_button").GetComponent<Button>();
        previousPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.allyShopPageTwoState));
    }

    public override void ExitSubState(ShopStateManager shopContext)
    {
        shopContext.allyShopPage3.SetActive(false);
        // shopContext.allyShop.SetActive(false);
    }
}

