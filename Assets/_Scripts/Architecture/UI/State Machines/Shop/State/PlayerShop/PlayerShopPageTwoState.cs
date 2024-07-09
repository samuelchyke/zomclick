using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;

public class PlayerShopPageTwoState : ShopBaseState
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    Button previousPageButton;
    public TextMeshProUGUI rallyAlliesCostText;
    public TextMeshProUGUI incendiaryRoundsCostText;
    public TextMeshProUGUI midasShotCostText;

    public Button rallyAlliesBuyButton;
    public Button incendiaryRoundsBuyButton;
    public Button midasShotBuyButton;

    Dictionary<string, TextMeshProUGUI> skillsCostTexts;

    public override void EnterState(ShopStateManager shopContext){}

    public override void ExitState(ShopStateManager shopContext){}

    public override void EnterSubState(ShopStateManager shopContext)
    {
        shopContext.playerShopPage2.SetActive(true);
        rallyAlliesCostText = GameObject.Find("rally_allies_gold_text").GetComponent<TextMeshProUGUI>();
        incendiaryRoundsCostText = GameObject.Find("incendiary_rounds_gold_text").GetComponent<TextMeshProUGUI>();
        midasShotCostText = GameObject.Find("midas_shot_gold_text").GetComponent<TextMeshProUGUI>();

        rallyAlliesBuyButton = GameObject.Find("rally_allies_buy_button").GetComponent<Button>();
        incendiaryRoundsBuyButton = GameObject.Find("incendiary_rounds_buy_button").GetComponent<Button>();
        midasShotBuyButton = GameObject.Find("midas_shot_buy_button").GetComponent<Button>();

        skillsCostTexts = new Dictionary<string, TextMeshProUGUI>
        {
            { "rally_allies_id", rallyAlliesCostText },
            { "incendiary_rounds_id", incendiaryRoundsCostText },
            { "midas_shot_id", midasShotCostText }
        };

        shopContext.playerShopViewModel.playerSkills
            .Subscribe(details => UpdateSkillsUI(details))
            .AddTo(_disposables);

        rallyAlliesBuyButton.onClick.AddListener(() => onBuy(shopContext, "rally_allies_id"));
        incendiaryRoundsBuyButton.onClick.AddListener(() => onBuy(shopContext, "incendiary_rounds_id"));
        midasShotBuyButton.onClick.AddListener(() => onBuy(shopContext, "midas_shot_id"));

        previousPageButton = GameObject.Find("previous_page_button").GetComponent<Button>();
        previousPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.playerShopPageOneState));
    }

    public override void ExitSubState(ShopStateManager shopContext)
    {
        shopContext.playerShopPage2.SetActive(false);
    }

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

    public void Dispose()
    {
        _disposables.Dispose();
        _disposables = new CompositeDisposable();
    }
}
