using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IEnemyRepository
{
    Task<EnemyStats> ReadEnemyStats();
    Task UpdateEnemyStats(EnemyStats enemyStats);
    Task<EnemyWaveDetails> ReadEnemyWaveDetails();
    Task UpdateEnemyWaveDetails(EnemyWaveDetails waveDetails);
    Task<BossStats> ReadBossStats();
    Task UpdateBossStats(BossStats enemyStats);
}

public class EnemyRepositoryImpl : IEnemyRepository, IInitializable
{
    IEnemyDao enemyDao;

    [Inject]
    public EnemyRepositoryImpl(IEnemyDao enemyDao)
    {
        this.enemyDao = enemyDao;
    }

    public void Initialize()
    {
        Debug.Log("Enemy Repository Initialized");
    }

    public async Task<EnemyStats> ReadEnemyStats()
    {
        EnemyStatsEntity entity = await enemyDao.ReadEnemyEntity();
        Debug.Log("Enemy Repository ReadEnemyStats:" + entity.id);
        return new EnemyStatsBuilder().ToDomain(entity);
    }

    public async Task UpdateEnemyStats(EnemyStats playerStats)
    {
        EnemyStatsEntity entity = new EnemyStatsBuilder().ToEntity(playerStats);
        await enemyDao.UpdateEnemyStats(entity);
    }

    public async Task<EnemyWaveDetails> ReadEnemyWaveDetails()
    {
        EnemyWaveEntity entity = await enemyDao.ReadEnemyWaveEntity();
        Debug.Log("Enemy Repository ReadEnemyWaveDetails:" + entity.id);
        return new EnemyWaveDetailsBuilder().ToDomain(entity);
    }

    public async Task UpdateEnemyWaveDetails(EnemyWaveDetails waveDetails)
    {
        EnemyWaveEntity entity = new EnemyWaveDetailsBuilder().ToEntity(waveDetails);
        await enemyDao.UpdateEnemyWaveDetails(entity);
    }

    public async Task<BossStats> ReadBossStats()
    {
        BossStatsEntity entity = await enemyDao.ReadBossStatsEntity();
        Debug.Log("Enemy Repository ReadBossStats:" + entity.id);
        return new BossStatsBuilder().ToDomain(entity);
    }

    public async Task UpdateBossStats(BossStats bossStats)
    {
        BossStatsEntity entity = new BossStatsBuilder().ToEntity(bossStats);
        await enemyDao.UpdateBossStats(entity);
    }
}