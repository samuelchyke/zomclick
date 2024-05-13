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
        wallHealthCostText = shopContext.wallHealthCostText;
        damageCostText = shopContext.damageCostText;
        critRateCostText = shopContext.critRateCostText;
        critDamageCostText = shopContext.critRateCostText;

        wallHealthBuyButton = shopContext.wallHealthBuyButton;
        damageBuyButton = shopContext.damageBuyButton;
        critRateBuyButton = shopContext.critRateBuyButton;
        critDamageBuyButton = shopContext.critDamageBuyButton;

        shopContext.playerUpgradeShop.SetActive(true);

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