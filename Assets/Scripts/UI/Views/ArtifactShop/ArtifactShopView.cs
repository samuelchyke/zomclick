using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.Views.ArtifactShop {
    public sealed class ArtifactShopView : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private GameObject _view;

        [Header("Text")]
        public TextMeshProUGUI _artifactUnlockCostText;

        [Header("Buttons")]
        public Button _artifactBuyButton;

        public void ShowView() => _view.SetActive(true);
        public void HideView() => _view.SetActive(false);

        public TextMeshProUGUI artifactUnlockCostText => _artifactUnlockCostText;
        public Button artifactBuyButton => _artifactBuyButton;
    }
}