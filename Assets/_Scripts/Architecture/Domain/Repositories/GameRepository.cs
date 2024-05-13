using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IGameRepository
{
    Task IncrementRound();
}

public class GameRepositoryImpl : IGameRepository, IInitializable
{
    IEnemyDao enemyDao;
    IPlayerStatsDao playerStatsDao;

    [Inject]
    public GameRepositoryImpl(
        IEnemyDao enemyDao,
        IPlayerStatsDao playerStatsDao
        )
    {
        this.enemyDao = enemyDao;
        this.playerStatsDao = playerStatsDao;
    }

    public void Initialize()
    {
        Debug.Log("Enemy Repository Initialized");
    }
    
    public async Task IncrementRound()
    {
        await IncrementEnemyStats();

        var enemyWaveEntity = await enemyDao.ReadEnemyWaveEntity();
        enemyWaveEntity.enemiesKilled = 0;
        enemyWaveEntity.round += 9;
        enemyWaveEntity.spawnLimit += 2;
        await enemyDao.UpdateEnemyWaveEntity(enemyWaveEntity);
    }

    async Task IncrementEnemyStats()
    {
        var enemyStats = await enemyDao.ReadEnemyEntity();
        enemyStats.totalHealth += 2;
        enemyStats.movementSpeed += 0.01f;
        await enemyDao.UpdateEnemyStats(enemyStats);
    }
}