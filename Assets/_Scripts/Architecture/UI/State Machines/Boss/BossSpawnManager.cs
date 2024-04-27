using System.Collections;
using UnityEngine;
using Zenject;

public class BossSpawnManager : MonoBehaviour
{
    [Inject(Id = "BossPrefab")] GameObject bossPrefab;
    [Inject] DiContainer _container;
    [Inject] EventsManager eventsManager;
    public const float SPAWN_TIMER = 5;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        eventsManager.StartListening(GameEvent.GameViewModelEvent.START_BOSS_ROUND, StartSpawnBoss);
    }

    private void StartSpawnBoss()
    {
        StartCoroutine(SpawnBoss());
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(SPAWN_TIMER);

        var bossGameObject = _container.InstantiatePrefab(bossPrefab, transform.position, transform.rotation, null);
        bossGameObject.GetComponent<BossStateManager>();
    }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.GameViewModelEvent.START_BOSS_ROUND, StartSpawnBoss);
    }
}