using System.Collections;
using UnityEngine;
using Zenject;

public class EnemySpawnManager : MonoBehaviour
{
    [Inject(Id = "EnemyPrefab")] GameObject enemyPrefab;
    [Inject] DiContainer _container;
    [Inject] EventsManager eventsManager;
    [Inject] IEnemyViewModel enemyViewModel;

    EnemyWaveDetails enemyWaveDetails;
    bool setUpComplete = false;

    private float timeElapsed = 0;
    public const float SPAWN_TIMER = 2;

    [SerializeField] int spawnTotal = 0;
    public BoxCollider2D boxCollider;  // Reference to the BoxCollider2D

    void Awake() 
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.ENEMY_VM_SETUP_COMPLETE, SetUp);
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_WAVE_DETAILS, UpdateEnemyWaveDetails);
        eventsManager.StartListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, Reset);
    }

    void SetUp()
    {
        enemyWaveDetails = enemyViewModel.enemyWaveDetails;
        StartSpawnEnemy();
        setUpComplete = true;
    }

    // void Start()
    // {
    //     StartSpawnEnemy;
    // } 

    void Update()
    {
        // if(!setUpComplete) return;
        // if (spawnTotal < enemyWaveDetails.spawnLimit)
        // if (timeElapsed < SPAWN_TIMER) 
        // {
        //     timeElapsed += Time.deltaTime;
        // } 
        // else 
        // {
        //     SpawnEnemy();
        //     timeElapsed = 0;
        // }
    }

    void StartSpawnEnemy()
    {
        // var enemyGameObject = _container.InstantiatePrefab(enemyPrefab, transform.position, transform.rotation, null);
        // enemyGameObject.GetComponent<EnemyStateManager>();
        // spawnTotal += 1;
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (spawnTotal < enemyWaveDetails.spawnLimit)
        {
            yield return new WaitForSeconds(SPAWN_TIMER);
            SpawnEnemyAtRandomPoint();
            // var enemyGameObject = _container.InstantiatePrefab(enemyPrefab, transform.position, transform.rotation, null);
            // enemyGameObject.GetComponent<EnemyStateManager>();
            spawnTotal += 1;
        }
    }

    void Reset()
    {
        timeElapsed = -3;
        spawnTotal = 0;
        StartSpawnEnemy();
    }

    void UpdateEnemyWaveDetails()
    {
        enemyWaveDetails = enemyViewModel.enemyWaveDetails; 
    }

    void SpawnEnemyAtRandomPoint()
    {
        Vector2 spawnPoint = CalculateSpawnPoint();
        // spawnPoint.y += GetEnemyHeight() / 2; 
        // spawnPoint.y = Mathf.Max(spawnPoint.y, minY + GetEnemyHeight() / 2);  // Adjust so the bottom edge is at minY
        var enemyGameObject = _container.InstantiatePrefab(enemyPrefab, spawnPoint, Quaternion.identity, null);
        enemyGameObject.GetComponent<EnemyStateManager>();
        var renderer = enemyGameObject.GetComponent<SpriteRenderer>();
        renderer.sortingOrder = Mathf.RoundToInt(spawnPoint.y * -100); 
    }

    float GetEnemyHeight()
    {
        // Assuming the enemy uses a BoxCollider2D for simplicity; adjust if different
        var collider = enemyPrefab.GetComponent<BoxCollider2D>();
        return collider.size.y * enemyPrefab.transform.localScale.y;
    }

    Vector2 CalculateSpawnPoint()
    {
        float minX = boxCollider.bounds.min.x;
        float maxX = boxCollider.bounds.max.x;
        float minY = boxCollider.bounds.min.y;
        float segmentWidth = (maxX - minX) / 10;
        int segmentIndex = Random.Range(0, 10);  // Choose a segment index from 0 to 9
        float randomX = minX + segmentIndex * segmentWidth + Random.Range(0, segmentWidth);
        float randomY = minY + Random.Range(0, boxCollider.bounds.size.y);  // Random height within the collider

        return new Vector2(randomX, randomY);
    }

    void OnDisable()
    {
        eventsManager.StopListening(GameEvent.EnemyViewModelEvent.ENEMY_VM_SETUP_COMPLETE, SetUp);
        eventsManager.StopListening(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_WAVE_DETAILS, UpdateEnemyWaveDetails);
        eventsManager.StopListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, Reset);
    }
}
