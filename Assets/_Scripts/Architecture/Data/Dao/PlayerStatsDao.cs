using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IPlayerStatsDao 
{
    Task<PlayerStatsEntity> ReadPlayerStats();

    Task UpdatePlayerStats(PlayerStatsEntity playerStats);

    Task UpdatePlayerGold(int goldAmount);
}

public class PlayerStatsDaoImpl : IPlayerStatsDao, IInitializable
{
    SQLiteConnection _db;
    DatabaseManager databaseManager;

    [Inject]
    public PlayerStatsDaoImpl (DatabaseManager databaseManager)
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
        var playerStats = _db.Table<PlayerStatsEntity>().First();
        Debug.Log("Player Dao - Read Player Stats: " + playerStats.id);
        return Task.Run(() => playerStats);
    }

    public Task UpdatePlayerStats(PlayerStatsEntity playerStats)
    {
        return Task.Run(() => _db.Update(playerStats));
    }

    public Task UpdatePlayerGold(int goldAmount)
    {
        return Task.Run(() => 
        {
            string query = "UPDATE PlayerStatsEntity SET totalGold = ?";
            _db.Execute(query, goldAmount);
            Debug.Log("Updated player gold to: " + goldAmount);
        });
    }
}