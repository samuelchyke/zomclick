using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Transactions;
using System.Threading;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Data.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Builders;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories {
    public interface IEnemyRepository
    {
        Task<EnemyStats> ReadEnemyStats();
        Task<EnemyWaveDetails> ReadEnemyWaveDetails();
        Task<BossStats> ReadBossStats();
        Task InflictDamage();
        Task OnEnemyDeath();
    }

    public class EnemyRepositoryImpl : IEnemyRepository, IInitializable
    {
        IEnemyDao enemyDao;
        IPlayerDao playerStatsDao;

        [Inject]
        public EnemyRepositoryImpl(
            IEnemyDao enemyDao,
            IPlayerDao playerStatsDao
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
            // playerStats.wallHealth -= bossStats.damage;
            await playerStatsDao.UpdatePlayerStats(playerStats);
        }

        private static readonly SemaphoreSlim enemyDeathSemaphore = new SemaphoreSlim(1, 1);

        public async Task OnEnemyDeath()
        {
            await enemyDeathSemaphore.WaitAsync();
            try
            {
                // Critical section starts
                await IncrementPlayerGold();
                var enemyWaveEntity = await enemyDao.ReadEnemyWaveEntity();
                enemyWaveEntity.enemiesKilled += 1;
                await enemyDao.UpdateEnemyWaveEntity(enemyWaveEntity);
                // Critical section ends
            }
            finally
            {
                enemyDeathSemaphore.Release();
            }
        }

        async Task IncrementPlayerGold()
        {
            var enemyStats = await enemyDao.ReadEnemyEntity();
            await playerStatsDao.IncreasePlayerGold(enemyStats.goldDropAmount);
        }
    }
}