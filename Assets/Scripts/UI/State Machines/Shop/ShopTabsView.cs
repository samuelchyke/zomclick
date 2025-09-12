using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop {
    public sealed class ShopTabsView : MonoBehaviour
    {
        [Header("Root")]
        [SerializeField] private GameObject _root;

        [Header("Currency")]
        [SerializeField] private TextMeshProUGUI _currencyText;

        [Header("Tabs")]
        [SerializeField] private Button _playerUpgradeShopButton;
        [SerializeField] private Button _allyShopButton;
        [SerializeField] private Button _artifactShopButton;

        public TextMeshProUGUI currencyText => _currencyText;
        public Button playerUpgradeShopButton => _playerUpgradeShopButton;
        public Button allyShopButton => _allyShopButton;
        public Button artifactShopButton => _artifactShopButton;
    }
}