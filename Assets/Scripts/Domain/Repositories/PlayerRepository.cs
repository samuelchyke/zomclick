using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Data.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Builders;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using UnityEngine;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories {
    public interface IPlayerRepository
    {
        Task<PlayerStats> ReadPlayerStats();

        // Task UpdatePlayerStats(PlayerStats playerStats);
    }

    public class PlayerRepositoryImpl : IPlayerRepository
    {
        private readonly IPlayerDao playerStatsDao;

        public PlayerRepositoryImpl(IPlayerDao playerStatsDao)
        {
            this.playerStatsDao = playerStatsDao;
        }

        public async Task<PlayerStats> ReadPlayerStats()
        {
            PlayerStatsEntity entity = await playerStatsDao.ReadPlayerStats();
            // Debug.Log("pl Repo:" + entity.id);
            return new PlayerStatsBuilder().ToDomain(entity);
        }

        // public async Task UpdatePlayerStats(PlayerStats playerStats)
        // {
        //     PlayerStatsEntity entity = new PlayerStatsBuilder().ToEntity(playerStats);
        //     await playerStatsDao.UpdatePlayerStats(entity);
        // }
    }
}
