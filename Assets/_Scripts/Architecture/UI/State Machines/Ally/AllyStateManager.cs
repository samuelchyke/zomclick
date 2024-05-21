using UnityEngine;
using Zenject;
using R3;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class AllyStateManager : MonoBehaviour
{
    [Inject(Id = "ProjectilePrefab")] readonly GameObject _projectilePrefab;

    [Inject] EventsManager eventsManager;
    [Inject] public DiContainer _container;
    [Inject] readonly AllyStateFactory states;
    [Inject] public IAllyViewModel allyViewModel;
    [Inject] public IAllyShopViewModel allyShopViewModel;

    AllyStats allyStats;
    public const string ATTACK_TRIGGER = "Attack";
    private Animator _animator;
    public Animator animator { get => _animator; }
    public GameObject projectilePrefab { get => _projectilePrefab; } 

    private AllyBaseState _currentState;
    public AllyBaseState currentState { get => _currentState; set { _currentState = value;} }
    public AllySpawnState spawnState;
    public AllyAttackState attackState;

    // public Transform launchOffest;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _animator = GetComponent<Animator>(); 
        spawnState = states.CreateSpawnState();
        attackState = states.CreateAttackState();
    }

    void OnEnable()
    {
        // eventsManager.StartListening(GameEvent.AllyShopViewModelEvent.SHOP_VM_SETUP_COMPLETE, SetUp);
    }

    // void Start()
    // {
    //     // allyViewModel.allyStats.Subscribe(stats => Spawn(stats));
    //     allyShopViewModel.allies.Subscribe(
    //         stats => Spawn(stats, "john_id")
    //     );

    // }

    void SetUp()
    {
        // allyViewModel.allyStats.Subscribe(stats => Spawn(stats));
        // allyShopViewModel.allies.Subscribe(
        //     stats => Spawn(stats, "john_id")
        // );
    }

    // private void Spawn(AllyStats stats)
    // {
    //     if (stats.isUnlocked)
    //     {
    //         currentState = spawnState;

    //         currentState.EnterState(this);
    //     }
    // }

    // private void Spawn(List<AllyStats> allies, string allyId)
    // {
    //     var allyIndex = allies.FindIndex(ally => ally.id == allyId);
    //     var ally = allies[allyIndex];

    //     if (ally.isUnlocked)
    //     {
    //         currentState = spawnState;

    //         currentState.EnterState(this);
    //     }
    // }

    void Update()
    {
        currentState?.UpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    public void SwitchState(AllyBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    // public void Instas()
    // {
    //     var projectile = container.InstantiatePrefab(ProjectilePrefab, launchOffest.position, launchOffest.rotation, null);
    //     projectile.GetComponent<AutoAimProjectile>();
    // }

    // public void FireProjectile()
    // {
    //     animator.SetTrigger(ATTACK_TRIGGER);
    //     // var projectile = _container.InstantiatePrefab(_projectilePrefab, launchOffest.position, launchOffest.rotation, null);
    //     // projectile.GetComponent<Projectile>();
    // }

    // public void OnDeath()
    // {
    //     currentHealth = playerViewModel.TakeDamage(currentHealth);
    //     if (currentHealth <= 0)
    //     {
    //         SwitchState(deadState);
    //     }
    // }

    // public void UpdatePlayerStats()
    // {
    //     playerStats = playerViewModel.playerStats;
    // }

    void OnDisable()
    {
        // eventsManager.StartListening(GameEvent.PlayerViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStats);
    }
}
