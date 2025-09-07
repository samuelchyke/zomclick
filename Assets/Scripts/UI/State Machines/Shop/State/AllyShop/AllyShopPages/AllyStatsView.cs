using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Shop.State.AllyShop.AllyShopPages {
    public sealed class AllyStatsView : MonoBehaviour
    {
        [Header("Stats Panel")]
        [SerializeField] private GameObject _allyStatsRoot;
        [SerializeField] private Button _allyStatsBackground;
        [SerializeField] private TextMeshProUGUI _allyStatsNameText;
        [SerializeField] private TextMeshProUGUI _allyStatsLevelText;
        [SerializeField] private TextMeshProUGUI _allyStatsDamageText;
        [SerializeField] private TextMeshProUGUI _allyStatsLoreText;
        [SerializeField] private TextMeshProUGUI _level10DescriptionText;
        [SerializeField] private TextMeshProUGUI _level25DescriptionText;
        [SerializeField] private GameObject _level10Cover;
        [SerializeField] private GameObject _level25Cover;

        public void ShowStats() => _allyStatsRoot.SetActive(true);
        public void HideStats() => _allyStatsRoot.SetActive(false);
        public Button allyStatsBackground => _allyStatsBackground;
        public TextMeshProUGUI allyStatsNameText => _allyStatsNameText;
        public TextMeshProUGUI allyStatsLevelText => _allyStatsLevelText;
        public TextMeshProUGUI allyStatsDamageText => _allyStatsDamageText;
        public TextMeshProUGUI allyStatsLoreText => _allyStatsLoreText;
        public TextMeshProUGUI level10DescriptionText => _level10DescriptionText;
        public TextMeshProUGUI level25DescriptionText => _level25DescriptionText;
        public GameObject level10Cover => _level10Cover;
        public GameObject level25Cover => _level25Cover;
    }
}