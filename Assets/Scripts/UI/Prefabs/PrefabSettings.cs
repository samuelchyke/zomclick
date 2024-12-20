using UnityEngine;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.Prefabs {
    [CreateAssetMenu(fileName = "PrefabSettings", menuName = "Settings/PrefabSettings")]
    public class PrefabSettings : ScriptableObject
    {
        public GameObject EnemyUIPrefab;
        public GameObject BossUIPrefab;
        public GameObject ProjectilePrefab;
        public GameObject JohnPrefab;
        public GameObject DoePrefab;
    }
}