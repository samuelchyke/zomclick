using System.Collections;
using UnityEngine;
using Zenject;
using System.Collections.Generic;
using R3;

public class AllySpawnManager : MonoBehaviour
{
    [Inject(Id = "JohnPrefab")] GameObject johnPrefab;
    [Inject(Id = "DoePrefab")] GameObject doePrefab;

    [Inject] public IAllyShopViewModel allyShopViewModel;

    private Dictionary<string, GameObject> _allyPrefabs;

    [Inject] DiContainer _container;
    [Inject] EventsManager eventsManager;
    public const float SPAWN_TIMER = 5;

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _allyPrefabs = new Dictionary<string, GameObject>
        {
            { "john_id", johnPrefab },
            { "doe_id", doePrefab },
        };
    }

    void OnEnable()
    {
        eventsManager.StartListening(GameEvent.AllyShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, SetUp);
        // eventsManager.StartListening(GameEvent.GameViewModelEvent.START_BOSS_ROUND, StartSpawnBoss);
    }

    void SetUp()
    {
        allyShopViewModel.allies.Subscribe(allies => Spawn(allies));
    }

    private void Spawn(List<AllyStats> allies)
    {
        Debug.Log("Spawn");
        foreach (var ally in allies) 
        {
            if (ally.isUnlocked)
            {   
                Debug.Log("Spawn " + ally.name);
                _allyPrefabs[ally.id].SetActive(true);
            //    _container.InstantiatePrefab(_allyPrefabs[ally.id].gameObject);
            }
        }
    }

    void OnDisable()
    {
        // eventsManager.StopListening(GameEvent.GameViewModelEvent.START_BOSS_ROUND, );
    }
}