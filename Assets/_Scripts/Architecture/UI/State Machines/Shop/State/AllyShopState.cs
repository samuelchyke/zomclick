using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;

public class AllyShopState : ShopBaseState, IDisposable
{
    CompositeDisposable _disposables = new CompositeDisposable();

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

    Dictionary<string, TextMeshProUGUI> _allyCostTexts;

    public override void EnterState(ShopStateManager shopContext)
    {
        shopContext.allyShop.SetActive(true);

        johnCostText = GameObject.Find("john_cost_text").GetComponent<TextMeshProUGUI>();
        johnBuyButton = GameObject.Find("john_buy_button").GetComponent<Button>();
        johnStatsButton = GameObject.Find("john_stats_button").GetComponent<Button>();

        doeCostText = GameObject.Find("doe_cost_text").GetComponent<TextMeshProUGUI>();
        doeBuyButton = GameObject.Find("doe_buy_button").GetComponent<Button>();
        doeStatsButton = GameObject.Find("doe_stats_button").GetComponent<Button>();

        _allyCostTexts = new Dictionary<string, TextMeshProUGUI>
        {
            { "john_id", johnCostText },
            { "doe_id", doeCostText }
        };

        shopContext.allyShopViewModel.allies
            .Subscribe(allies => UpdateUI(allies))
            .AddTo(_disposables);

        doeBuyButton.onClick.AddListener(() => onBuy(shopContext, "doe_id"));
        doeStatsButton.onClick.AddListener(() => showStats(shopContext, "doe_id"));

        johnBuyButton.onClick.AddListener(() => onBuy(shopContext, "john_id"));
        johnStatsButton.onClick.AddListener(() => showStats(shopContext, "john_id"));
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

    private void UpdateUI(List<AllyStats> allies)
    {
        // johnCostText.text = allies[].unlockCost.ToString();
        foreach (var ally in allies)
        {
            if (_allyCostTexts.TryGetValue(ally.id, out var costText))
            {
                costText.text = ally.isUnlocked ? ally.upgradeCost.ToString() : ally.unlockCost.ToString();
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
}
