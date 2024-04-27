using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public interface IGameViewModel
{
    EnemyWaveDetails enemyWaveDetails { get; }
}

public class GameViewModelImpl : IGameViewModel, IInitializable
{
    IReadPlayerStatsUseCase readPlayerStatsUseCase;
    IReadEnemyWaveDetailsUseCase readEnemyWaveDetailsUseCase;
    EventsManager eventsManager;


    public GameViewModelImpl(
        IReadPlayerStatsUseCase readPlayerStatsUseCase,
        IReadEnemyWaveDetailsUseCase readEnemyWaveDetailsUseCase,
        EventsManager eventsManager
        )
    {
        this.readPlayerStatsUseCase = readPlayerStatsUseCase;
        this.readEnemyWaveDetailsUseCase = readEnemyWaveDetailsUseCase;
        this.eventsManager = eventsManager;
    }

    PlayerStats _playerStats { get; set; }
    EnemyWaveDetails _enemyWaveDetails { get; set; }
    public EnemyWaveDetails enemyWaveDetails { get => _enemyWaveDetails; }

    public async void Initialize()
    {
        _playerStats = await readPlayerStatsUseCase.Invoke();
        _enemyWaveDetails = await readEnemyWaveDetailsUseCase.Invoke();
        
        eventsManager.StartListening(GameEvent.PlayerViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStatsEvent);
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.UPDATE_ENEMY_WAVE_DETAILS, UpdateEnemyWaveDetailsEvent);

        Debug.Log("Game ViewModel Initialized");
    }

    public void StartNextRound()
    {
        eventsManager.TriggerEvent(GameEvent.GameViewModelEvent.START_NEXT_ROUND);
    }

    public void StartBossRoundCheck ()
    {
        Debug.Log($"Current Round: {_enemyWaveDetails.round}");
        if (_enemyWaveDetails.round % 10 == 0)
        {
            Debug.Log($"Current Round: {_enemyWaveDetails.round % 10}");
            eventsManager.TriggerEvent(GameEvent.GameViewModelEvent.START_BOSS_ROUND);
        }
    }

    public void GameOver()
    {
        if(_playerStats.wallHealth == 0)
        {
            eventsManager.TriggerEvent(GameEvent.GameViewModelEvent.GAME_OVER);
        }
    }

    async void UpdatePlayerStatsEvent()
    {
        var newPlayerStats = await readPlayerStatsUseCase.Invoke();
        if (newPlayerStats != _playerStats)
        {
            _playerStats = newPlayerStats;
        }
    }

    async void UpdateEnemyWaveDetailsEvent()
    {
        var newEnemyWaveDetails = await readEnemyWaveDetailsUseCase.Invoke();
        if (newEnemyWaveDetails != _enemyWaveDetails)
        {
            _enemyWaveDetails = newEnemyWaveDetails;
            StartNextRound();
            StartBossRoundCheck();
        }
    }
}
