using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;
using R3;
using System.Collections.Generic;

#nullable enable
public class ShopStateManager : MonoBehaviour
{

    [Inject] EventsManager eventsManager;
    [Inject] readonly ShopStateFactory states;
    [Inject] public IPlayerShopViewModel playerShopViewModel;
    [Inject] public IAllyShopViewModel allyShopViewModel; 

    private ShopBaseState _currentState;
    public ShopBaseState currentState { get => _currentState; set { _currentState = value;} }

    private ShopBaseState? _currentSubState;
    public ShopBaseState? currentSubState { get => _currentSubState; set { _currentSubState = value;} }

    public ShopTabState shopTabState;
    public PlayerShopState playerShopState;
    public PlayerShopPageOneState playerShopPageOneState;
    public PlayerShopPageTwoState playerShopPageTwoState;

    public AllyShopState allyShopState;
    public AllyShopPageOneState allyShopPageOneState;
    public AllyShopPageTwoState allyShopPageTwoState;
    public AllyShopPageThreeState allyShopPageThreeState;

    public TextMeshProUGUI goldText;
    public Button playerUpgradeShopButton;
    public Button allyShopButton;

    public GameObject playerUpgradeShop;
    public GameObject playerShopPage1;
    public GameObject playerShopPage2;

    public GameObject allyShop;

    public TextMeshProUGUI johnCostText;
    public Button johnBuyButton;
    public Button johnStatsButton;

    public GameObject allyShopPage1;
    public GameObject allyShopPage2;
    public GameObject allyShopPage3;
    public GameObject allyStats;

    public GameObject shopTab;

    Dictionary<ShopType, GameObject> shopTabs;
    

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        shopTabState = states.CreateShopTabState();
        playerShopState = states.CreatePlayerShopState();
        playerShopPageOneState = states.CreatePlayerShopPageOneState();
        playerShopPageTwoState = states.CreatePlayerShopPageTwoState();
        allyShopState = states.CreateAllyShopState();
        allyShopPageOneState = states.CreateAllyShopPageOneState();
        allyShopPageTwoState = states.CreateAllyShopPageTwoState();
        allyShopPageThreeState = states.CreateAllyShopPageThreeState();

        // shopTabs = new Dictionary<ShopType, GameObject>
        // {
        //     { ShopType.ShopTab, shopTab },
        //     { ShopType.PlayerShop, playerUpgradeShop },
        //     { ShopType.AllyShop, allyShopPage1 },
        //     { ShopType.AllyShop, allyShopPage2 },
        //     { ShopType.AllyShop, allyShopPage3 }
        // };
    }

    void OnEnable()
    {
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, InitializeShopUI);
    }

    private void InitializeShopUI()
    {
        playerShopViewModel.shopDetails.Subscribe(details => goldText.text = details.totalGold.ToString());
    }

    void Start()
    {   
        currentState = shopTabState;
        currentState.EnterState(this);

        // currentSubState = allyShopPageOneState;

        playerUpgradeShopButton.onClick.AddListener(() => SwitchStates(playerShopState)); 
        allyShopButton.onClick.AddListener(() => SwitchStates(allyShopState)); 
    }

    public void Cleanup()
    {
        eventsManager.StopListening(GameEvent.PlayerShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, InitializeShopUI);
    }

    protected void SwitchStates(ShopBaseState newState){
        if (currentState != newState)
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

public enum ShopType
{
    PlayerShop,
    AllyShop,
    ShopTab
}
