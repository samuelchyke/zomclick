using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IEnemyRepository
{
    Task<EnemyStats> ReadEnemyStats();
    Task<EnemyWaveDetails> ReadEnemyWaveDetails();
    Task<BossStats> ReadBossStats();
    Task InflictDamage();
    Task OnEnemyDeath();
    Task IncrementRound();
}

public class EnemyRepositoryImpl : IEnemyRepository, IInitializable
{
    IEnemyDao enemyDao;
    IPlayerStatsDao playerStatsDao;

    [Inject]
    public EnemyRepositoryImpl(
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
        await enemyDao.UpdateEnemyWaveEntity(entity);
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

    public async Task InflictDamage()
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var bossStats = await enemyDao.ReadBossStatsEntity();
        playerStats.wallHealth -= bossStats.damage;
        await playerStatsDao.UpdatePlayerStats(playerStats);
    }

    public async Task OnEnemyDeath()
    {
        await IncrementPlayerGold();
        var enemyWaveEntity = await enemyDao.ReadEnemyWaveEntity();
        enemyWaveEntity.enemiesKilled += 1;
        await enemyDao.UpdateEnemyWaveEntity(enemyWaveEntity);
    }

    async Task IncrementPlayerGold()
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var enemyStats = await enemyDao.ReadEnemyEntity();
        playerStats.totalGold += enemyStats.goldDropAmount;
        await playerStatsDao.UpdatePlayerStats(playerStats);
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