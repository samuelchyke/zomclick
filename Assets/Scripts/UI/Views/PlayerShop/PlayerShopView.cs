using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.Views.PlayerShop {
    public class PlayerShopView : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private GameObject _view;

        [Header("Player Upgrade")]
        [SerializeField] private TextMeshProUGUI _playerUpgradeCostText;
        [SerializeField] private Button _playerUpgradeBuyButton;

        public TextMeshProUGUI playerUpgradeCostText => _playerUpgradeCostText;
        public Button playerUpgradeBuyButton => _playerUpgradeBuyButton;

        public void ShowView() => _view.SetActive(true);
        public void HideView() => _view.SetActive(false);
    }
}