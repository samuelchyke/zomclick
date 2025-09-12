using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.Views.AllyShop {
    public sealed class AllyShopPageOneView : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private GameObject _view;

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

        public void ShowView() => _view.SetActive(true);
        public void HideView() => _view.SetActive(false);
        
        public Button nextPageButton => _nextPageButton;
        public TextMeshProUGUI johnCostText => _johnCostText;
        public Button johnBuyButton => _johnBuyButton;
        public Button johnStatsButton => _johnStatsButton;
        public TextMeshProUGUI doeCostText => _doeCostText;
        public Button doeBuyButton => _doeBuyButton;
        public Button doeStatsButton => _doeStatsButton;
    }
}