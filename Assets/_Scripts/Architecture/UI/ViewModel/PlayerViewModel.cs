using UnityEngine;
using Zenject;

public interface IPlayerViewModel
{
    PlayerStats playerStats { get; }
    void OnDeath();
}
public class PlayerViewModelImpl : IPlayerViewModel, IInitializable
{
    readonly IReadPlayerStatsUseCase readPlayerStatsUseCase;
    readonly EventsManager eventsManager;

    [Inject]
    public PlayerViewModelImpl(
        IReadPlayerStatsUseCase readPlayerStatsUseCase, 
        EventsManager eventsManager
        )
    {
        this.readPlayerStatsUseCase = readPlayerStatsUseCase;
        this.eventsManager = eventsManager;
    }

    public async void Initialize()
    {
        _playerStats = await readPlayerStatsUseCase.Invoke();

        Debug.Log("PLayer View Model Initialized");
        eventsManager.StartListening(GameEvent.PlayerShopViewModelEvent.UPDATE_PLAYER_STATS, UpdatePlayerStatsEvent);
        eventsManager.StartListening(GameEvent.EnemyViewModelEvent.INFLICT_DAMAGE, TakeDamage);
        // eventsManager.StartListening(GameEvent.GameManagerEvent.RESTART_ROUND, Reset);
    }

    PlayerStats _playerStats;
    public PlayerStats playerStats => _playerStats;

    public async void UpdatePlayerStats()
    {
        // await updatePlayerStatsUseCase.Invoke(_playerStats);
        _playerStats = await readPlayerStatsUseCase.Invoke();
    }
    
    public void TakeDamage()
    {
        _playerStats.wallHealth -= 1;
        UpdatePlayerStats();
    }

    public void OnDeath()
    {
        eventsManager.TriggerEvent(GameEvent.GameViewModelEvent.GAME_OVER);
    }

    async void UpdatePlayerStatsEvent()
    {
        var newPlayerStats = await readPlayerStatsUseCase.Invoke();
        if (newPlayerStats != _playerStats)
        {
            _playerStats = newPlayerStats;
        }
    }
}

