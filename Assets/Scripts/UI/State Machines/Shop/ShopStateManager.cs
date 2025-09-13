using UnityEngine;
using Zenject;
using TMPro;
using UnityEngine.UI;
using R3;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events;
using Com.Studio.Zomclick.Assets.Scripts.UI.Views;
using Com.Studio.Zomclick.Assets.Scripts.UI.Views.AllyShop;
using Com.Studio.Zomclick.Assets.Scripts.UI.Views.ArtifactShop;
using Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.PlayerShop.PlayerShopPages;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.PlayerShop;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.AllyShop;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.AllyShop.AllyShopPages;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.ArtifactShop;
using Com.Studio.Zomclick.Assets.Scripts.UI.Views.PlayerShop;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State {
    #nullable enable
    public class ShopStateManager : MonoBehaviour
    {
        [Inject] EventsManager eventsManager;
        [Inject] readonly ShopStateFactory states;
        [Inject] public IPlayerShopViewModel playerShopViewModel;
        [Inject] public IAllyShopViewModel allyShopViewModel; 
        [Inject] public IArtifactShopViewModel artifactShopViewModel; 

        private ShopBaseState _currentState;
        public ShopBaseState currentState { get => _currentState; set { _currentState = value;} }

        private ShopBaseState? _currentSubState;
        public ShopBaseState? currentSubState { get => _currentSubState; set { _currentSubState = value;} }

        public ShopTabState shopTabState;

        [Header("Player Shop View")]
        public PlayerShopView playerShopView;
        public PlayerShopState playerShopState;
        public PlayerShopPageOneState playerShopPageOneState;
        public PlayerShopPageTwoState playerShopPageTwoState;

        [Header("Ally Shop View")]
        public AllyShopState allyShopState;
        public AllyShopPageOneState allyShopPageOneState;
        public AllyShopPageOneView allyShopPageOneView;
        public AllyShopPageTwoState allyShopPageTwoState;
        public AllyShopPageThreeState allyShopPageThreeState;
        public AllyStatsView allyStatsView;

        public ArtifactShopState artifactShopState;
        public ArtifactShopUnlockedPageState artifactShopPageUnlockedState;
        public ArtifactShopLockedPageState artifactShopPageLockedState;

        [Header("Shop Tabs View")]
        public ShopTabsView shopTabsView;

        [Header("Artifact Shop View")]
        public ArtifactShopView artifactShopView;
        public ArtifactShopLockedPageView artifactShopLockedPageView;

        public GameObject artifactShop;
        public GameObject artifactShopLockedPage;
        public GameObject artifactShopUnlockedPage;

        public GameObject playerUpgradeShop;
        public GameObject playerShopPage1;
        public GameObject playerShopPage2;

        public GameObject allyShop;

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

            artifactShopState = states.CreateArtifactShopState();
            artifactShopPageUnlockedState = states.CreateArtifactUnlockedPageState();
            artifactShopPageLockedState = states.CreateArtifactLockedPageState();

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
            SubscribeGold();
        }

        public void SubscribeGold() 
        {
            playerShopViewModel.shopDetails.Subscribe(details => shopTabsView.currencyText.text = details.totalGold.ToString());
        }

        void Start()
        {   
            currentState = shopTabState;
            currentState.EnterState(this);

            shopTabsView.playerUpgradeShopButton.onClick.AddListener(() => SwitchStates(playerShopState)); 
            shopTabsView.allyShopButton.onClick.AddListener(() => SwitchStates(allyShopState)); 
            shopTabsView.artifactShopButton.onClick.AddListener(() => SwitchStates(artifactShopState));
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
}