using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;
using R3;

public class ShopStateManager : MonoBehaviour
{

    [Inject] EventsManager eventsManager;
    [Inject] readonly ShopStateFactory states;
    [Inject] public IPlayerShopViewModel playerShopViewModel;
    // [Inject] public IAllyViewModel allyViewModel;

    private ShopBaseState _currentState;
    public ShopBaseState currentState { get => _currentState; set { _currentState = value;} }

    public ShopTabState shopTabState;
    public PlayerShopState playerShopState;
    public AllyShopState allyShopState;

    public TextMeshProUGUI goldText;
    public Button playerUpgradeShopButton;
    public Button allyShopButton;

    public GameObject playerUpgradeShop;
    public GameObject allyShop;
    public GameObject shopTab;

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

        shopTabState = states.CreateShopTabState();
        playerShopState = states.CreatePlayerShopState();
        allyShopState = states.CreateAllyShopState();
    }

    void OnEnable()
    {
        eventsManager.StartListening(GameEvent.ShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, InitializeShopUI);
    }

    private void InitializeShopUI()
    {
        // playerShopViewModel.shopDetails.Subscribe(details => UpdateUI(details));
        playerShopViewModel.shopDetails.Subscribe(details => goldText.text = details.totalGold.ToString());
    }

    private void UpdateUI(PlayerShopDetails details)
    {
        goldText.text = details.totalGold.ToString();
        wallHealthCostText.text = details.wallHealthCost.ToString();
        damageCostText.text = details.damageCost.ToString();
        critRateCostText.text = details.critRateCost.ToString();
        critDamageCostText.text = details.critDamageCost.ToString();
    }

    void Start()
    {   
        currentState = shopTabState;
        currentState.EnterState(this);

        // playerShopViewModel.shopDetails.Subscribe(details => UpdateUI(details));
        playerUpgradeShopButton.onClick.AddListener(() => SwitchStates(playerShopState)); 
        // allyShopButton.onClick.AddListener(() => SwitchState(allyShopState)); 
    }

    public void Cleanup()
    {
        eventsManager.StopListening(GameEvent.ShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, InitializeShopUI);
    }

    protected void SwitchStates(ShopBaseState newState){
        if (currentState == shopTabState)
        {
            currentState.ExitState(this);
            currentState = newState;
            currentState.EnterState(this);
        }
        else
        {
            currentState.ExitState(this);
            currentState = shopTabState;
            currentState.EnterState(this);
        }
    }
}
