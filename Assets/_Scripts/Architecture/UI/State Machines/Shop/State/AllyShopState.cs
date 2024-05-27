using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;
using System;
using System.Collections.Generic;

public class AllyShopState : ShopBaseState, IDisposable
{
    private CompositeDisposable _disposables = new CompositeDisposable();

    public TextMeshProUGUI johnCostText;
    public TextMeshProUGUI doeCostText;

    public Button johnBuyButton;
    public Button doeBuyButton;

    private Dictionary<string, TextMeshProUGUI> _allyCostTexts;


    public override void EnterState(ShopStateManager shopContext)
    {
        shopContext.allyShop.SetActive(true);

        johnCostText = GameObject.Find("john_cost_text").GetComponent<TextMeshProUGUI>();
        doeCostText = GameObject.Find("doe_cost_text").GetComponent<TextMeshProUGUI>();

        johnBuyButton = GameObject.Find("john_buy_button").GetComponent<Button>();
        doeBuyButton = GameObject.Find("doe_buy_button").GetComponent<Button>();

        _allyCostTexts = new Dictionary<string, TextMeshProUGUI>
        {
            { "john_id", johnCostText },
            { "doe_id", doeCostText }
        };

        shopContext.allyShopViewModel.allies
            .Subscribe(allies => UpdateUI(allies))
            .AddTo(_disposables);

        johnBuyButton.onClick.AddListener(() => onBuy(shopContext, "john_id"));
        doeBuyButton.onClick.AddListener(() => onBuy(shopContext, "doe_id"));
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