using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using System.Linq;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using R3;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao {
    public interface IPlayerDao 
    {
        Observable<PlayerStatsEntity> ObservePlayerStats();
        Task<PlayerStatsEntity> ReadPlayerStats();
        Task<List<PlayerSkillEntity>> ReadPlayerSkills();
        Task<PlayerSkillEntity> ReadPlayerSkill(string playerSkillId);

        Task UpdatePlayerStats(PlayerStatsEntity playerStats);
        Task UpdatePlayerSkill(PlayerSkillEntity playerSkillEntity);

        Task IncreasePlayerGold(int goldToAdd);
    }

    public class PlayerDaoImpl : IPlayerDao, IInitializable
    {
        readonly SQLiteConnection _db;
        
        private BehaviorSubject<PlayerStatsEntity> _playerStats;

        public PlayerDaoImpl(SQLiteConnection databaseManager)
        {
            _db = databaseManager;
            _ = InitializeObservables();
        }

        public void Initialize()
        {
            Debug.Log("Player Dao Initialized");
        }

        private async Task InitializeObservables()
        {
            var entity = await ReadPlayerStats();
            _playerStats = new BehaviorSubject<PlayerStatsEntity>(entity);
        }

        public Observable<PlayerStatsEntity> ObservePlayerStats()
        {
            return _playerStats;
        }
    
        public Task<PlayerStatsEntity> ReadPlayerStats()
        {
            return Task.Run(() =>
            {
                string query = "SELECT * FROM playerStats LIMIT 1";
                return _db.Query<PlayerStatsEntity>(query).First();
            });
        }

        public Task<List<PlayerSkillEntity>> ReadPlayerSkills()
        {
            return Task.Run(() => 
            {
                string query = "SELECT * FROM playerSkills";
                return _db.Query<PlayerSkillEntity>(query);
            });
        }

        public Task<PlayerSkillEntity> ReadPlayerSkill(string playerSkillId)
        {
            return Task.Run(() => 
            {
                string query = $"SELECT * FROM playerSkills WHERE id = ?";
                return _db.Query<PlayerSkillEntity>(query, playerSkillId).First();
            });
        }

        public async Task UpdatePlayerStats(PlayerStatsEntity playerStats)
        {
            await Task.Run(() => _db.Update(playerStats)).ConfigureAwait(false);
            _playerStats.OnNext(await ReadPlayerStats().ConfigureAwait(false));
        }

        public Task UpdatePlayerSkill(PlayerSkillEntity playerSkillEntity)
        {
            return Task.Run(() => _db.Update(playerSkillEntity));
        }

        public async Task IncreasePlayerGold(int goldToAdd)
        {
            await Task.Run(() =>
            {
                string query = @"
                    UPDATE playerStats
                    SET totalGold = totalGold + ?
                    WHERE id = (
                        SELECT id
                        FROM playerStats
                        LIMIT 1
                    )";

                _db.Execute(query, goldToAdd);
            }).ConfigureAwait(false);
            _playerStats.OnNext(await ReadPlayerStats().ConfigureAwait(false));
        }
    }
}