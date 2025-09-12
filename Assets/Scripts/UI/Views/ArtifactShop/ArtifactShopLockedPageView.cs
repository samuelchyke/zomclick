using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.Views.ArtifactShop {
    public sealed class ArtifactShopLockedPageView : MonoBehaviour
    {
        [Header("View")]
        [SerializeField] private GameObject _view;

        public void ShowView() => _view.SetActive(true);
        public void HideView() => _view.SetActive(false);
    }
}