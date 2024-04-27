using System.Threading.Tasks;
using UnityEngine;

public interface IPlayerRepository
{
    Task<PlayerStats> ReadPlayerStats();

    Task UpdatePlayerStats(PlayerStats playerStats);
}

public class PlayerRepositoryImpl : IPlayerRepository
{
    private readonly IPlayerStatsDao playerStatsDao;

    public PlayerRepositoryImpl(IPlayerStatsDao playerStatsDao)
    {
        this.playerStatsDao = playerStatsDao;
    }

    public async Task<PlayerStats> ReadPlayerStats()
    {
        PlayerStatsEntity entity = await playerStatsDao.ReadPlayerStats();
        // Debug.Log("pl Repo:" + entity.id);
        return new PlayerStatsBuilder().ToDomain(entity);
    }

    public async Task UpdatePlayerStats(PlayerStats playerStats)
    {
        PlayerStatsEntity entity = new PlayerStatsBuilder().ToEntity(playerStats);
        await playerStatsDao.UpdatePlayerStats(entity);
    }
}
