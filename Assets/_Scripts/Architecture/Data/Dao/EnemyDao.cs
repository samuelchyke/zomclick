using SQLite4Unity3d;
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
        var enemyEntity = _db.Table<EnemyStatsEntity>().First();
        Debug.Log("Enemy Dao - ReadEnemyDetails: " + enemyEntity.id);
        return Task.Run(() => _db.Table<EnemyStatsEntity>().First());
    }

     public Task UpdateEnemyStats(EnemyStatsEntity enemyEntity)
    {
        return Task.Run(() =>_db.Update(enemyEntity));
    }

    public Task<EnemyWaveEntity> ReadEnemyWaveEntity()
    {
        var enemyEntity = _db.Table<EnemyWaveEntity>().First();
        Debug.Log("Enemy Dao - ReadEnemyDetails: " + enemyEntity.id);
        return Task.Run(() => _db.Table<EnemyWaveEntity>().First());
    }

    public Task UpdateEnemyWaveEntity(EnemyWaveEntity enemyWaveEntity)
    {
        return Task.Run(() =>_db.Update(enemyWaveEntity));
    }

    public Task<BossStatsEntity> ReadBossStatsEntity()
    {
        return Task.Run(() => _db.Table<BossStatsEntity>().First());
    }

    public Task UpdateBossStats(BossStatsEntity bossStatsEntity)
    {
        return Task.Run(() => _db.Update(bossStatsEntity));
    }
}