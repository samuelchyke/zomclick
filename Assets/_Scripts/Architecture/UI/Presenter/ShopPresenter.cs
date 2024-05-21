using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;
using R3;

public class ShopPresenter : MonoBehaviour
{
    [Inject] EventsManager eventsManager;
    [Inject] readonly IPlayerShopViewModel shopViewModel;
    // Observable<PlayerUpgradeShopDetails> shopDetails;
    ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails;

    public TextMeshProUGUI goldText;
    public Button playerUpgradeShopButton;
    public Button allyShopButton;

    public GameObject playerUpgradeShop;
    public GameObject allyShop;
    public GameObject shopTab;

    bool isPlayerUpgradeShopToggled = false;
    bool isAllyShopToggled = false;

    public TextMeshProUGUI wallHealthCostText;
    public TextMeshProUGUI damageCostText;
    public TextMeshProUGUI critRateCostText;
    public TextMeshProUGUI critDamageCostText;

    public Button wallHealthBuyButton;
    public Button damageBuyButton;
    public Button critRateBuyButton;
    public Button critDamageBuyButton;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable() 
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, InitializeShopUI);
        // eventsManager.StartListening(GameEvent.ShopViewModelEvent.UPDATE_SHOP_DETAILS, SetTexts);
    }
    void Start()
    {
        // shopViewModel.shopDetails.Subscribe(details => UpdateUI(details));

        // .Subscribe( detail => 
        //     shopDetails.Value = detail
        // );

        // InitializeShopUI();
        // eventsManager.StartListening(GameEvent.ShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, InitializeShopUI);
        // Subscribe to shop details once and handle all UI updates here
        // shopDetails = shopViewModel.shopDetails;
        // shopDetails.Subscribe(details =>
        // {
        //     goldText.text = details.totalGold.ToString();
        //     wallHealthCostText.text = details.wallHealthCost.ToString();
        //     damageCostText.text = details.damageCost.ToString();
        //     critRateCostText.text = details.critRateCost.ToString();
        //     critDamageCostText.text = details.critDamageCost.ToString();
        // });

        // Initialize interactive elements
        // InitializeShopUI();  
        // SetButtonsInteractable(false);
    }
    
    void SetTexts(){
        // shopDetails = shopViewModel.shopDetails;
        // shopDetails.Subscribe( 
        //     details => 
        //         damageCostText.text = details.damageCost.ToString()
        // );        

        // damageCostText.text = shopDetails.CurrentValue.damageCost.ToString();
    }

    private void UpdateUI(PlayerShopDetails details)
    {
        goldText.text = details.totalGold.ToString();
        wallHealthCostText.text = details.wallHealthCost.ToString();
        damageCostText.text = details.damageCost.ToString();
        critRateCostText.text = details.critRateCost.ToString();
        critDamageCostText.text = details.critDamageCost.ToString();
        Debug.Log("UI Updated with new details");
    }

    void InitializeShopUI(){
        playerUpgradeShopButton.onClick.AddListener(TogglePlayerUpgradeShop); 
        allyShopButton.onClick.AddListener(ToggleAllyShop); 
        wallHealthBuyButton.onClick.AddListener(shopViewModel.BuyWallHealth);
        damageBuyButton.onClick.AddListener(shopViewModel.BuyDamage);
        critRateBuyButton.onClick.AddListener(shopViewModel.BuyCritRate);
        critDamageBuyButton.onClick.AddListener(shopViewModel.BuyCritDamage);

        shopViewModel.shopDetails.Subscribe(details => UpdateUI(details));

        // shopViewModel.shopDetails.Subscribe(details => UpdateUI(details));


        // .Subscribe( detail => 
        //     shopDetails.Value = detail
        // );

        // InitializeShopUI();
        // SetTexts();
    }

    public void TogglePlayerUpgradeShop()
    {
        if (!isPlayerUpgradeShopToggled)
        {
            shopTab.SetActive(false);
            playerUpgradeShop.SetActive(true);
            // SetButtonsInteractable(true); 
            isPlayerUpgradeShopToggled = true;
        }
        else
        {
            playerUpgradeShop.SetActive(false);
            shopTab.SetActive(true);
            // SetButtonsInteractable(false); 
            isPlayerUpgradeShopToggled = false;
        }
    }

    private void SetButtonsInteractable(bool state)
    {
        wallHealthBuyButton.interactable = state;
        damageBuyButton.interactable = state;
        critRateBuyButton.interactable = state;
        critDamageBuyButton.interactable = state;
    }

    public void ToggleAllyShop()
    {
        if (!isAllyShopToggled)
        {
            shopTab.SetActive(false);
            allyShop.SetActive(true);
            isAllyShopToggled = true;
        }
        else
        {
            allyShop.SetActive(false);
            shopTab.SetActive(true);
            isAllyShopToggled = false;
        }
    }

    public void Cleanup()
    {
        eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, InitializeShopUI);
        // eventsManager.StopListening(GameEvent.ShopViewModelEvent.UPDATE_SHOP_DETAILS, SetTexts);
    }
}