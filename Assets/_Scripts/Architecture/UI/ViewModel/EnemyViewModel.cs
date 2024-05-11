using UnityEngine;
using Zenject;
using R3;

public interface IEnemyViewModel
{
    ReadOnlyReactiveProperty<EnemyStats> enemyStats { get; }
    ReadOnlyReactiveProperty<EnemyWaveDetails> enemyWaveDetails { get; }

    void UpdateEnemyStats();
    void OnDeath();
}

public class EnemyViewModelImpl : IEnemyViewModel, IInitializable
{
    IReadEnemyStatsUseCase readEnemyStatsUseCase;
    IReadEnemyWaveDetailsUseCase readEnemyWaveDetailsUseCase;
    IOnEnemyDeathUseCase onEnemyDeathUseCase;
    EventsManager eventsManager;

    [Inject]
    public EnemyViewModelImpl(
        IReadEnemyStatsUseCase readEnemyStatsUseCase,
        IReadEnemyWaveDetailsUseCase readEnemyWaveDetailsUseCase,
        IOnEnemyDeathUseCase onEnemyDeathUseCase,
        EventsManager eventsManager       
        )
    {
        this.readEnemyStatsUseCase = readEnemyStatsUseCase;
        this.readEnemyWaveDetailsUseCase = readEnemyWaveDetailsUseCase;
        this.onEnemyDeathUseCase = onEnemyDeathUseCase;
        this.eventsManager = eventsManager;
    }

    ReactiveProperty<EnemyStats> _enemyStats = new ();
    ReactiveProperty<EnemyWaveDetails> _enemyWaveDetails = new();

    public ReadOnlyReactiveProperty<EnemyStats> enemyStats => _enemyStats;
    public ReadOnlyReactiveProperty<EnemyWaveDetails> enemyWaveDetails => _enemyWaveDetails;

    private ReactiveProperty<PlayerShopDetails> _shopDetails = new ();
    public ReadOnlyReactiveProperty<PlayerShopDetails> shopDetails => _shopDetails;

    public async void Initialize()
    {
        _enemyStats.Value = await readEnemyStatsUseCase.Invoke();
        _enemyWaveDetails.Value = await readEnemyWaveDetailsUseCase.Invoke();

        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.ENEMY_VM_SETUP_COMPLETE);
        eventsManager.StartListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, UpdateEnemyStats);
        eventsManager.StartListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, UpdateEnemyWaveDetails);

        Debug.Log("Enemy ViewModel Initialized");
    }

    public async void UpdateEnemyStats()
    {
        _enemyStats.Value = await readEnemyStatsUseCase.Invoke();
        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_STATS);
    }

    public async void UpdateEnemyWaveDetails()
    {
        _enemyWaveDetails.Value = await readEnemyWaveDetailsUseCase.Invoke();
        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_WAVE_DETAILS);
    }

    public async void OnDeath()
    {
        await onEnemyDeathUseCase.Invoke();
        UpdateEnemyStats();
        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.ON_DEATH);
    }

    public void Cleanup()
    {
        // eventsManager.StopListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, IncrementRound);
    }
}