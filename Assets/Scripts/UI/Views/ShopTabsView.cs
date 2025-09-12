using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.Views {
    public class ShopTabsView : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private GameObject _view;

        [Header("Currency Text")]
        [SerializeField] private TextMeshProUGUI _currencyText;

        [Header("Shop Tabs")]
        [SerializeField] private Button _playerUpgradeShopButton;
        [SerializeField] private Button _allyShopButton;
        [SerializeField] private Button _artifactShopButton;

        public TextMeshProUGUI currencyText => _currencyText;
        public Button playerUpgradeShopButton => _playerUpgradeShopButton;
        public Button allyShopButton => _allyShopButton;
        public Button artifactShopButton => _artifactShopButton;
    }
}