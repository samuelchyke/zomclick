using UnityEngine;
using System.IO;
using SQLite4Unity3d;
using Debug = UnityEngine.Debug;
using Zenject;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database {
    public class DatabaseManager : IInitializable
    {
        public SQLiteConnection dbConnection { get; private set; }
        private const int CurrentDbVersion = 1;

        public void Initialize()
        {
            InitializeDatabase();
        }

        public void InitializeDatabase()
        {
            string folderPath = Path.GetDirectoryName(Application.persistentDataPath);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string dbPath = Path.Combine(Application.persistentDataPath, "game.db");
            
            ResetDb(dbPath, resetDb: false);

            dbConnection = new SQLiteConnection(dbPath);

            SoftResetDb(resetDb: true);

            SetDb(setDb: true);
            
            var jsonSeeder = new JsonSeeder(
                seedDao : new SeedDaoImpl(dbConnection),
                seedEntityUpdater : new SeedEntityUpdaterImpl(dbConnection)
            );

            jsonSeeder.Seed("dump.json");

            new Migrations(dbConnection, CurrentDbVersion).ApplyMigrations(applyMigrations: false);

            Debug.Log(Application.persistentDataPath);
        }

        void SetDb(bool setDb)
        {
            if (!setDb) return; 
            dbConnection.CreateTable<MetadataEntity>();
            dbConnection.CreateTable<PlayerStatsEntity>();
            dbConnection.CreateTable<PlayerSkillEntity>();
            dbConnection.CreateTable<EnemyStatsEntity>();
            dbConnection.CreateTable<EnemyWaveEntity>();
            dbConnection.CreateTable<BossStatsEntity>();
            dbConnection.CreateTable<PlayerShopEntity>();
            dbConnection.CreateTable<AllyStatsEntity>();
            dbConnection.CreateTable<AllySkillEntity>();
        }

        void ResetDb(string dbPath, bool resetDb)
        {
            if (!resetDb) return;
            if (File.Exists(dbPath))
            {
                Debug.Log("Database file found. Deleting to reset...");
                File.Delete(dbPath);
            }
            else
            {
                Debug.Log("No existing database file found. Creating new database...");
            }
        }

        void SoftResetDb(bool resetDb)
        {
            if (!resetDb) return; 
            dbConnection.DropTable<MetadataEntity>();
            dbConnection.DropTable<PlayerStatsEntity>();
            dbConnection.DropTable<PlayerSkillEntity>();
            dbConnection.DropTable<PlayerShopEntity>();
            dbConnection.DropTable<EnemyStatsEntity>();
            dbConnection.DropTable<BossStatsEntity>();
            dbConnection.DropTable<EnemyWaveEntity>();
            dbConnection.DropTable<AllyStatsEntity>();
            dbConnection.DropTable<AllySkillEntity>();
        }
    }
}