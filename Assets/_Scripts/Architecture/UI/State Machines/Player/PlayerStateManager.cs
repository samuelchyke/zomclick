using UnityEngine;
using Zenject;

public class PlayerStateManager : MonoBehaviour
{
    [Inject(Id = "ProjectilePrefab")] readonly GameObject _projectilePrefab;


    [Inject] EventsManager eventsManager;
    [Inject] public DiContainer _container;
    [Inject] readonly PlayerStateFactory states;
    [Inject] public IPlayerViewModel playerViewModel;
    PlayerStats playerStats;
    public const string ATTACK_TRIGGER = "Attack";
    private Animator _animator;
    public Animator animator { get => _animator; }
    public GameObject projectilePrefab { get => _projectilePrefab; } 


    private PlayerBaseState _currentState;
    public PlayerBaseState currentState { get => _currentState; set { _currentState = value;} }
    public PlayerIdleState idleState;
    public PlayerAttackState attackState;
    public PlayerDeadState deadState;

    public Transform launchOffest;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        _animator = GetComponent<Animator>(); 
        idleState = states.CreateIdleState();
        attackState = states.CreateAttackState();
        deadState = states.CreateDeadState();
    }

    void OnEnable()
    {
        eventsManager.StartListening(GameEvent.PlayerViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStats);
    }

    void Start()
    {   
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter2D(this, collision);
    }

    public void SwitchState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    // public void Instas()
    // {
    //     var projectile = container.InstantiatePrefab(ProjectilePrefab, launchOffest.position, launchOffest.rotation, null);
    //     projectile.GetComponent<AutoAimProjectile>();
    // }

    public void FireProjectile()
    {
        animator.SetTrigger(ATTACK_TRIGGER);
        var projectile = _container.InstantiatePrefab(_projectilePrefab, launchOffest.position, launchOffest.rotation, null);
        projectile.GetComponent<Projectile>();
    }

    // public void OnDeath()
    // {
    //     currentHealth = playerViewModel.TakeDamage(currentHealth);
    //     if (currentHealth <= 0)
    //     {
    //         SwitchState(deadState);
    //     }
    // }

    public void UpdatePlayerStats()
    {
        playerStats = playerViewModel.playerStats;
    }

    void OnDisable()
    {
        eventsManager.StartListening(GameEvent.PlayerViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStats);
    }
}
