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

        SetUI();

        var playerSkills = shopContext.playerShopViewModel.playerSkills.CurrentValue;

        skillsCostTexts = new Dictionary<string, TextMeshProUGUI>
        {
            { playerSkills.rallyAllies.id, rallyAlliesCostText },
            { playerSkills.incendiaryRounds.id, incendiaryRoundsCostText },
            { playerSkills.midasRounds.id, midasShotCostText }
        };

        shopContext.playerShopViewModel.playerSkills.Subscribe(skills => 
            {
                playerSkills = skills;
                UpdateSkillsUI(skills);
            }
        )
        .AddTo(_disposables);

        rallyAlliesBuyButton.onClick.AddListener(() => onBuy(shopContext, playerSkills.rallyAllies));
        incendiaryRoundsBuyButton.onClick.AddListener(() => onBuy(shopContext, playerSkills.incendiaryRounds));
        midasShotBuyButton.onClick.AddListener(() => onBuy(shopContext, playerSkills.midasRounds));

        previousPageButton = GameObject.Find("previous_page_button").GetComponent<Button>();
        previousPageButton.onClick.AddListener(() => SwitchSubStates(shopContext, shopContext.playerShopPageOneState));
    }

    public override void ExitSubState(ShopStateManager shopContext)
    {
        shopContext.playerShopPage2.SetActive(false);
    }

    private void onBuy (ShopStateManager shopContext, PlayerSkill playerSkill)
    {
        if (playerSkill.isUnlocked){
            shopContext.playerShopViewModel.UpgradePlayerSkill(playerSkill.id);
        }
        else
        {
            shopContext.playerShopViewModel.UnlockPlayerSkill(playerSkill.id);
        }
    }

    private void UpdateSkillsUI(PlayerSkills playerSkills)
    {
        UpdateSkillUI(playerSkills.rallyAllies);
        UpdateSkillUI(playerSkills.incendiaryRounds);
        UpdateSkillUI(playerSkills.midasRounds);
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
        rallyAlliesCostText = GameObject.Find("rally_allies_gold_text").GetComponent<TextMeshProUGUI>();
        incendiaryRoundsCostText = GameObject.Find("incendiary_rounds_gold_text").GetComponent<TextMeshProUGUI>();
        midasShotCostText = GameObject.Find("midas_shot_gold_text").GetComponent<TextMeshProUGUI>();

        rallyAlliesBuyButton = GameObject.Find("rally_allies_buy_button").GetComponent<Button>();
        incendiaryRoundsBuyButton = GameObject.Find("incendiary_rounds_buy_button").GetComponent<Button>();
        midasShotBuyButton = GameObject.Find("midas_shot_buy_button").GetComponent<Button>();
    }

    public void Dispose()
    {
        _disposables.Dispose();
        _disposables = new CompositeDisposable();
    }
}
