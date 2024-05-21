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
    [Inject] public IAllyShopViewModel allyShopViewModel; 

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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        shopTabState = states.CreateShopTabState();
        playerShopState = states.CreatePlayerShopState();
        allyShopState = states.CreateAllyShopState();
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
