using UnityEngine;
using TMPro;
using UnityEngine.UI;
using R3;

public class PlayerShopState : ShopBaseState
{

    public TextMeshProUGUI wallHealthCostText;
    public TextMeshProUGUI damageCostText;
    public TextMeshProUGUI critRateCostText;
    public TextMeshProUGUI critDamageCostText;

    public Button wallHealthBuyButton;
    public Button damageBuyButton;
    public Button critRateBuyButton;
    public Button critDamageBuyButton;

    public override void EnterState(ShopStateManager shopContext)
    {
        shopContext.playerUpgradeShop.SetActive(true);

        wallHealthCostText = GameObject.Find("health_gold_text").GetComponent<TextMeshProUGUI>();
        damageCostText = GameObject.Find("damage_gold_text").GetComponent<TextMeshProUGUI>();
        critRateCostText = GameObject.Find("crit_rate_gold_text").GetComponent<TextMeshProUGUI>();
        critDamageCostText = GameObject.Find("crit_damage_gold_text").GetComponent<TextMeshProUGUI>();

        wallHealthBuyButton = GameObject.Find("health_buy_button").GetComponent<Button>();
        damageBuyButton = GameObject.Find("damage_buy_button").GetComponent<Button>();
        critRateBuyButton = GameObject.Find("crit_rate_buy_button").GetComponent<Button>();
        critDamageBuyButton = GameObject.Find("crit_damage_buy_button").GetComponent<Button>();

        shopContext.playerShopViewModel.shopDetails.Subscribe(details => UpdateUI(details));

        wallHealthBuyButton.onClick.AddListener(shopContext.playerShopViewModel.BuyWallHealth);
        damageBuyButton.onClick.AddListener(shopContext.playerShopViewModel.BuyDamage);
        critRateBuyButton.onClick.AddListener(shopContext.playerShopViewModel.BuyCritRate);
        critDamageBuyButton.onClick.AddListener(shopContext.playerShopViewModel.BuyCritDamage);
    }

    public override void ExitState(ShopStateManager shopContext)
    {
        shopContext.playerUpgradeShop.SetActive(false);

        wallHealthBuyButton.onClick.RemoveListener(shopContext.playerShopViewModel.BuyWallHealth);
        damageBuyButton.onClick.RemoveListener(shopContext.playerShopViewModel.BuyDamage);
        critRateBuyButton.onClick.RemoveListener(shopContext.playerShopViewModel.BuyCritRate);
        critDamageBuyButton.onClick.RemoveListener(shopContext.playerShopViewModel.BuyCritDamage);
    }

    private void UpdateUI(PlayerShopDetails details)
    {
        wallHealthCostText.text = details.wallHealthCost.ToString();
        damageCostText.text = details.damageCost.ToString();
        critRateCostText.text = details.critRateCost.ToString();
        critDamageCostText.text = details.critDamageCost.ToString();
    }
}