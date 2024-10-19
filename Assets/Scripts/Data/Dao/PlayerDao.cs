using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using System.Linq;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Dao {
    public interface IPlayerDao 
    {
        Task<PlayerStatsEntity> ReadPlayerStats();
        Task<List<PlayerSkillEntity>> ReadPlayerSkills();
        Task<PlayerSkillEntity> ReadPlayerSkill(string playerSkillId);

        Task UpdatePlayerStats(PlayerStatsEntity playerStats);
        Task UpdatePlayerSkill(PlayerSkillEntity playerSkillEntity);

        Task IncreasePlayerGold(int goldToAdd);
    }

    public class PlayerDaoImpl : IPlayerDao, IInitializable
    {
        SQLiteConnection _db;
        DatabaseManager databaseManager;

        [Inject]
        public PlayerDaoImpl (DatabaseManager databaseManager)
        {
            this.databaseManager = databaseManager;
        }

        public void Initialize()
        {
            _db = databaseManager.dbConnection;
            Debug.Log("Player Dao Initialized");
        }
    
        public Task<PlayerStatsEntity> ReadPlayerStats()
        {
            // var playerStats = _db.Table<PlayerStatsEntity>().First();
            // Debug.Log("Player Dao - Read Player Stats: " + playerStats.id);
            // return Task.Run(() => playerStats);

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
                string query = $"SELECT * FROM playerSkills";
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

        public Task UpdatePlayerStats(PlayerStatsEntity playerStats)
        {
            return Task.Run(() => _db.Update(playerStats));
        }

        public Task UpdatePlayerSkill(PlayerSkillEntity playerSkillEntity)
        {
            return Task.Run(() => _db.Update(playerSkillEntity));
        }

        public Task IncreasePlayerGold(int goldToAdd)
        {
            return Task.Run(() => 
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
            });
        }
    }
}