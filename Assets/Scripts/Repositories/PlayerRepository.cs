using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using R3;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories {
    public interface IPlayerRepository
    {
        Task<PlayerStats> ReadPlayerStats();
        Observable<PlayerStats> ObservePlayerStats();
    }

    public class PlayerRepositoryImpl : IPlayerRepository
    {
        private readonly IPlayerDao playerStatsDao;

        public PlayerRepositoryImpl(IPlayerDao playerStatsDao)
        {
            this.playerStatsDao = playerStatsDao;
        }

        public Observable<PlayerStats> ObservePlayerStats()
        {
            return playerStatsDao.ObservePlayerStats().Select(entity =>
                new PlayerStatsBuilder().ToDomain(entity)
            );
        }

        public async Task<PlayerStats> ReadPlayerStats()
        {
            PlayerStatsEntity entity = await playerStatsDao.ReadPlayerStats();
            return new PlayerStatsBuilder().ToDomain(entity);
        }
    }
}
