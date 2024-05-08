using UnityEngine;
using Zenject;

public interface IEnemyViewModel
{
    EnemyStats enemyStats { get; }
    EnemyWaveDetails enemyWaveDetails { get; }

    void UpdateEnemyStats();
    void OnDeath();
    void InflictDamage();
    void IncrementRound();
    void IncrementStats();
}

public class EnemyViewModelImpl : IEnemyViewModel, IInitializable
{
    IReadPlayerStatsUseCase readPlayerStatsUseCase;
    IReadEnemyStatsUseCase readEnemyStatsUseCase;
    IReadEnemyWaveDetailsUseCase readEnemyWaveDetailsUseCase;
    IUpdateEnemyStatsUseCase updateEnemyStatsUseCase;
    IUpdateEnemyWaveDetailsUseCase updateEnemyWaveDetailsUseCase;
    EventsManager eventsManager;

    [Inject]
    public EnemyViewModelImpl(
        IReadPlayerStatsUseCase readPlayerStatsUseCase,
        IReadEnemyStatsUseCase readEnemyStatsUseCase,
        IUpdateEnemyStatsUseCase updateEnemyStatsUseCase,
        IReadEnemyWaveDetailsUseCase readEnemyWaveDetailsUseCase,
        IUpdateEnemyWaveDetailsUseCase updateEnemyWaveDetailsUseCase,
        EventsManager eventsManager       
        )
    {
        this.readPlayerStatsUseCase = readPlayerStatsUseCase;
        this.readEnemyStatsUseCase = readEnemyStatsUseCase;
        this.updateEnemyStatsUseCase = updateEnemyStatsUseCase;
        this.readEnemyWaveDetailsUseCase = readEnemyWaveDetailsUseCase;
        this.updateEnemyWaveDetailsUseCase = updateEnemyWaveDetailsUseCase;
        this.eventsManager = eventsManager;
    }

    EnemyStats _enemyStats { get; set; }
    EnemyWaveDetails _enemyWaveDetails { get; set; }

    public EnemyStats enemyStats => _enemyStats;
    public EnemyWaveDetails enemyWaveDetails => _enemyWaveDetails;

    public async void Initialize()
    {
        _enemyStats = await readEnemyStatsUseCase.Invoke();
        _enemyWaveDetails = await readEnemyWaveDetailsUseCase.Invoke();

        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.ENEMY_VM_SETUP_COMPLETE);
        Debug.Log("Enemy ViewModel Initialized");
    }

    public async void UpdateEnemyStats()
    {
        await updateEnemyStatsUseCase.Invoke(_enemyStats);
        _enemyStats = await readEnemyStatsUseCase.Invoke();
        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_STATS);
    }

    public async void UpdateEnemyWaveDetails()
    {
        await updateEnemyWaveDetailsUseCase.Invoke(_enemyWaveDetails);
        _enemyWaveDetails = await readEnemyWaveDetailsUseCase.Invoke();
        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_WAVE_DETAILS);
    }

    public void InflictDamage()
    {
        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.INFLICT_DAMAGE);
    }

    public void OnDeath()
    {
        eventsManager.TriggerEvent(GameEvent.EnemyViewModelEvent.ON_DEATH);
        _enemyWaveDetails.enemiesKilled += 1;
        if(_enemyWaveDetails.enemiesKilled == _enemyWaveDetails.spawnLimit)
        {
            IncrementRound();
        }
    }

    public void IncrementRound()
    {
        _enemyWaveDetails.enemiesKilled = 0;
        _enemyWaveDetails.round += 9;
        _enemyWaveDetails.spawnLimit += 2;
        UpdateEnemyWaveDetails();
        IncrementStats();
    }

    public void IncrementStats()
    {
        _enemyStats.damage += 1;
        _enemyStats.totalHealth += 2;
        _enemyStats.movementSpeed += 0.01f;
        UpdateEnemyStats();
    }

    public void Cleanup()
    {
        eventsManager.StopListening(GameEvent.GameViewModelEvent.START_NEXT_ROUND, IncrementRound);
    }
}