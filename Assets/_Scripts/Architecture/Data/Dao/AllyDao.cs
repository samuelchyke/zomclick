using SQLite4Unity3d;
using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.Collections.Generic;

public interface IAllyDao 
{
    Task<List<AllyStatsEntity>> ReadAlliesStats();
    Task<AllyStatsEntity> ReadAllyEntity(string allyId);
    public Task UpdateAllyStats(AllyStatsEntity allyEntity);
}

public class AllyDaoImpl : IAllyDao, IInitializable
{
    DatabaseManager databaseManager;
    SQLiteConnection _db;

    [Inject]
    public AllyDaoImpl (DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public void Initialize()
    {
        _db = databaseManager.dbConnection;
        Debug.Log("Ally Dao Initialized");
    }

    public Task<List<AllyStatsEntity>> ReadAlliesStats()
    {
        return Task.Run(() => 
        {
            string query = $"SELECT * FROM AllyStatsEntity";
            return _db.Query<AllyStatsEntity>(query);
        });
    }

    public Task<AllyStatsEntity> ReadAllyEntity(string allyId)
    {
        return Task.Run(() => 
        {
            string query = $"SELECT * FROM AllyStatsEntity WHERE id = ?";
            return _db.Query<AllyStatsEntity>(query, allyId).First();
        });
    }

    public Task UpdateAllyStats(AllyStatsEntity allyEntity)
    {
        return Task.Run(() =>_db.Update(allyEntity));
    }
}