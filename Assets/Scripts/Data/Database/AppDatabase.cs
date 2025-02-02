using UnityEngine;
using System.IO;
using SQLite4Unity3d;
using Debug = UnityEngine.Debug;
using Zenject;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Testing;
using System.Threading.Tasks;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database {

    public interface IAppDatabase
    {
        IAllyDao AllyDao();
        IPlayerDao PlayerDao();
        IPlayerShopDao PlayerShopDao();
        IEnemyDao EnemyDao();
        IArtifactDao ArtifactDao();

        void Close();
        Task RunInTransaction(System.Action action);
        SQLiteCommand CompileStatement(string sql);
    }

    public class AppDatabaseImpl : IAppDatabase, IInitializable
    {
        public SQLiteConnection dbConnection { get; private set; }

        public void Initialize()
        {
            InitializeDatabase();
        }


        public IStubDao StubDao() => new StubDaoImpl(dbConnection);
        public IAllyDao AllyDao() => new AllyDaoImpl(dbConnection);
        public IPlayerDao PlayerDao() => new PlayerDaoImpl(dbConnection);
        public IPlayerShopDao PlayerShopDao() => new PlayerShopDaoImpl(dbConnection);
        public IEnemyDao EnemyDao() => new EnemyDaoImpl(dbConnection);
        public IArtifactDao ArtifactDao() => new ArtifactDaoImpl(dbConnection);

        private const int CurrentDbVersion = 1;

        public void InitializeInMemory(SQLiteConnection inMemoryConnection)
        {
            dbConnection = inMemoryConnection;
            SetDb(true);
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
            dbConnection.CreateTable<ArtifactEntity>();
            dbConnection.CreateTable<ArtifactShopEntity>();
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
            dbConnection.DropTable<ArtifactEntity>();
            dbConnection.DropTable<ArtifactShopEntity>();
        }

        // Expose the SQLite connection close method
        public void Close()
        {
            dbConnection.Close();
        }

        // Expose running a transaction for testing
        public async Task RunInTransaction(System.Action action)
        {
            await Task.Run(() => dbConnection.RunInTransaction(action));
        }

        // Expose compiling a raw SQL statement for testing
        public SQLiteCommand CompileStatement(string sql)
        {
            return dbConnection.CreateCommand(sql);
        }
    }
}