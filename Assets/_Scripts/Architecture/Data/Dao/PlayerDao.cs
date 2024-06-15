using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Collections.Generic;
using System.Linq;

public interface IPlayerDao 
{
    Task<PlayerStatsEntity> ReadPlayerStats();
    Task<List<PlayerSkillEntity>> ReadPlayerSkills();
    Task<PlayerSkillEntity> ReadPlayerSkill(string playerSkillId);

    Task UpdatePlayerStats(PlayerStatsEntity playerStats);
    Task UpdatePlayerSkill(PlayerSkillEntity playerSkillEntity);
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
        var playerStats = _db.Table<PlayerStatsEntity>().First();
        Debug.Log("Player Dao - Read Player Stats: " + playerStats.id);
        return Task.Run(() => playerStats);
    }

    public Task<List<PlayerSkillEntity>> ReadPlayerSkills()
    {
        return Task.Run(() => 
        {
            string query = $"SELECT * FROM PlayerSkillEntity";
            return _db.Query<PlayerSkillEntity>(query);
        });
    }

    public Task<PlayerSkillEntity> ReadPlayerSkill(string playerSkillId)
    {
        return Task.Run(() => 
        {
            string query = $"SELECT * FROM PlayerSkillEntity WHERE id = ?";
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
}