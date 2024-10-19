using UnityEngine;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.Player.Skills {
    public class BigBettySpawner : MonoBehaviour
    {
        [Inject(Id = "BigBettyPrefab")] private GameObject bigBettyPrefab;
        [Inject] private DiContainer container;

        public GameObject spawnPoint;

        public void SpawnBigBetty()
        {
            // container.InstantiatePrefab(bigBettyPrefab, spawnPoint.position, spawnPoint.rotation, null);
        }
    }
}