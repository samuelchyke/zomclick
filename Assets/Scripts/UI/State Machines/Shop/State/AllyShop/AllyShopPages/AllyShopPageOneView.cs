using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.AllyShop.AllyShopPages {
    public sealed class AllyShopPageOneView : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private GameObject _root;

        [Header("Navigation")]
        [SerializeField] private Button _nextPageButton;

        [Header("John")]
        [SerializeField] private TextMeshProUGUI _johnCostText;
        [SerializeField] private Button _johnBuyButton;
        [SerializeField] private Button _johnStatsButton;

        [Header("Doe")]
        [SerializeField] private TextMeshProUGUI _doeCostText;
        [SerializeField] private Button _doeBuyButton;
        [SerializeField] private Button _doeStatsButton;

        public void ShowPage() => _root.SetActive(true);
        public void HidePage() => _root.SetActive(false);
        
        public Button nextPageButton => _nextPageButton;
        public TextMeshProUGUI johnCostText => _johnCostText;
        public Button johnBuyButton => _johnBuyButton;
        public Button johnStatsButton => _johnStatsButton;
        public TextMeshProUGUI doeCostText => _doeCostText;
        public Button doeBuyButton => _doeBuyButton;
        public Button doeStatsButton => _doeStatsButton;
    }
}