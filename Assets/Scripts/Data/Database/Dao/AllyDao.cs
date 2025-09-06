using SQLite4Unity3d;
using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao {
    public interface IAllyDao 
    {
        Task<List<AllyStatsEntity>> ReadAlliesStats();
        Task<List<AllySkillEntity>> ReadAlliesSkills();
        Task<AllyStatsEntity> ReadAllyEntity(string allyId);
        Task<List<AllySkillEntity>> ReadAllySkills(string allyId);

        public Task UpdateAllyStats(AllyStatsEntity allyEntity);
        public Task UpdateAllySkill(AllySkillEntity allySkillEntity);
    }

    public class AllyDaoImpl : IAllyDao, IInitializable
    {
        AppDatabaseImpl databaseManager;
        SQLiteConnection _db;

        public AllyDaoImpl (SQLiteConnection databaseManager)
        {
            this._db = databaseManager;
        }

        public void Initialize()
        {
            // _db = databaseManager.dbConnection;
            Debug.Log("Ally Dao Initialized");
        }

        public Task<List<AllyStatsEntity>> ReadAlliesStats()
        {
             if (_db == null)
            {
                Debug.LogError("Database connection is not initialized.");
                return Task.FromResult(new List<AllyStatsEntity>());
            }   

            return Task.Run(() => 
            {
                string query = $"SELECT * FROM allyStats";
                return _db.Query<AllyStatsEntity>(query);
            });
        }

        public Task<List<AllySkillEntity>> ReadAlliesSkills()
        {
            return Task.Run(() => 
            {
                string query = $"SELECT * FROM allySkills";
                return _db.Query<AllySkillEntity>(query);
            });
        }

        public Task<List<AllySkillEntity>> ReadAllySkills(string allyId)
        {
            return Task.Run(() => 
            {
                string query = $"SELECT * FROM allySkills WHERE allyId = ?";
                return _db.Query<AllySkillEntity>(query, allyId);
            });
        }

        public Task<AllyStatsEntity> ReadAllyEntity(string allyId)
        {
            return Task.Run(() => 
            {
                string query = $"SELECT * FROM allyStats WHERE id = ?";
                return _db.Query<AllyStatsEntity>(query, allyId).First();
            });
        }

        public Task UpdateAllyStats(AllyStatsEntity allyEntity)
        {
            return Task.Run(() =>_db.Update(allyEntity));
        }

        public Task UpdateAllySkill(AllySkillEntity allySkillEntity)
        {
            return Task.Run(() =>_db.Update(allySkillEntity));
        }
    }
}