using UnityEngine;
using System.IO;
using SQLite4Unity3d;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Zenject;

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
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        string folderPath = Path.GetDirectoryName(Application.persistentDataPath);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string dbPath = Path.Combine(Application.persistentDataPath, "game.db");
        
        ResetDb(dbPath, resetDb: false);

        dbConnection = new SQLiteConnection(dbPath);

        ResetDb(resetDb: true);

        SetDb(setDb: true);
        
        new Seeding(dbConnection).SeedDatabase(seedDb: true);
        new Migrations(dbConnection, CurrentDbVersion).ApplyMigrations(applyMigrations: false);

        stopwatch.Stop();
        Debug.Log($"Database initialized in {stopwatch.ElapsedMilliseconds} ms.");
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

    void ResetDb(bool resetDb)
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