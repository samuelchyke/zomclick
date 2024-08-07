using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;

public class PlayerShopPageOneState : ShopBaseState
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    Button nextPageButton;

    public TextMeshProUGUI bigBettyCostText;
    public TextMeshProUGUI turretCostText;
    public TextMeshProUGUI critRateCostText;

    public Button bigBettyBuyButton;
    public Button turretBuyButton;
    public Button critRateBuyButton;

    Dictionary<string, TextMeshProUGUI> skillsCostTexts;

    public override void EnterState(ShopStateManager shopContext){}

    private void onBuy (ShopStateManager shopContext, string playerSkillId)
    {
        var skillIndex = shopContext.playerShopViewModel.playerSkills.CurrentValue.FindIndex(skill => skill.id == playerSkillId);
        var playerSkill = shopContext.playerShopViewModel.playerSkills.CurrentValue[skillIndex];

        if (playerSkill.isUnlocked){
            shopContext.playerShopViewModel.UpgradePlayerSkill(playerSkillId);
        }
        else
        {
            shopContext.playerShopViewModel.UnlockPlayerSkill(playerSkillId);
        }
    }

    public override void ExitState(ShopStateManager shopContext){}

    public override void EnterSubState(ShopStateManager shopContext)
    {
        shopContext.playerShopPage1.SetActive(true);
        bigBettyCostText = GameObject.Find("big_betty_gold_text").GetComponent<TextMeshProUGUI>();
        turretCostText = GameObject.Find("turret_gold_text").GetComponent<TextMeshProUGUI>();
        critRateCostText = GameObject.Find("crit_rate_gold_text").GetComponent<TextMeshProUGUI>();

        bigBettyBuyButton = GameObject.Find("big_betty_buy_button").GetComponent<Button>();
        turretBuyButton = GameObject.Find("turret_buy_button").GetComponent<Button>();
        critRateBuyButton = GameObject.Find("crit_rate_buy_button").GetComponent<Button>();

        skillsCostTexts = new Dictionary<string, TextMeshProUGUI>
        {
            { "big_betty_id", bigBettyCostText },
            { "turret_id", turretCostText },
            { "crit_rate_id", critRateCostText }
        };

        shopContext.playerShopViewModel.playerSkills
            .Subscribe(details => UpdateSkillsUI(details))
            .AddTo(_disposables);

        bigBettyBuyButton.onClick.AddListener(() => onBuy(shopContext, "big_betty_id"));
        turretBuyButton.onClick.AddListener(() => onBuy(shopContext, "turret_id"));
        critRateBuyButton.onClick.AddListener(() => onBuy(shopContext, "crit_rate_id"));

        nextPageButton = GameObject.Find("next_page_button").GetComponent<Button>();
        nextPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.playerShopPageTwoState));
    }

    private void UpdateSkillsUI(List<PlayerSkill> playerSkills)
    {
        foreach (var skill in playerSkills)
        {
            if (skillsCostTexts.TryGetValue(skill.id, out var costText))
            {
                costText.text = skill.isUnlocked ? skill.upgradeCost.ToString() : skill.unlockCost.ToString();
            }
        }
    }

    public override void ExitSubState(ShopStateManager shopContext)
    {
        shopContext.playerShopPage1.SetActive(false);
    }
}

