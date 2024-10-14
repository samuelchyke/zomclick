using SQLite4Unity3d;
using System.Linq;
using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IEnemyDao 
{
    Task<EnemyStatsEntity> ReadEnemyEntity();
    public Task UpdateEnemyStats(EnemyStatsEntity enemyEntity);
    Task<EnemyWaveEntity> ReadEnemyWaveEntity();
    public Task UpdateEnemyWaveEntity(EnemyWaveEntity enemyWaveEntity);
    Task<BossStatsEntity> ReadBossStatsEntity();
    public Task UpdateBossStats(BossStatsEntity enemyWaveEntity);
}

public class EnemyDaoImpl : IEnemyDao, IInitializable
{
    DatabaseManager databaseManager;
    SQLiteConnection _db;

    [Inject]
    public EnemyDaoImpl (DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public void Initialize()
    {
        _db = databaseManager.dbConnection;
        Debug.Log("Enemy Dao Initialized");
    }

    public Task<EnemyStatsEntity> ReadEnemyEntity()
    {
        return Task.Run(() =>
        {
            string query = "SELECT * FROM enemyStats LIMIT 1";
            return _db.Query<EnemyStatsEntity>(query).First();
        });
    }

     public Task UpdateEnemyStats(EnemyStatsEntity enemyEntity)
    {
        return Task.Run(() =>_db.Update(enemyEntity));
    }

    public Task<EnemyWaveEntity> ReadEnemyWaveEntity()
    {
        return Task.Run(() =>
        {
            string query = "SELECT * FROM enemyWaves LIMIT 1";
            return _db.Query<EnemyWaveEntity>(query).First();
        });
    }

    public Task UpdateEnemyWaveEntity(EnemyWaveEntity enemyWaveEntity)
    {
        return Task.Run(() =>_db.Update(enemyWaveEntity));
    }

    public Task<BossStatsEntity> ReadBossStatsEntity()
    {
        return Task.Run(() =>
        {
            string query = "SELECT * FROM bossStats LIMIT 1";
            return _db.Query<BossStatsEntity>(query).First();
        });
    }

    public Task UpdateBossStats(BossStatsEntity bossStatsEntity)
    {
        return Task.Run(() => _db.Update(bossStatsEntity));
    }
}