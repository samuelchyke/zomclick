using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using SQLite4Unity3d;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Testing {
public interface IStubDao
{
    Task InsertAllySkill(AllySkillEntity entity);
    Task InsertAllyStats(AllyStatsEntity entity);
    Task InsertBossStats(BossStatsEntity entity);
    Task InsertEnemyStats(EnemyStatsEntity entity);
    Task InsertEnemyWave(EnemyWaveEntity entity);
    Task InsertMetadata(MetadataEntity entity);
    Task InsertPlayerShop(PlayerShopEntity entity);
    Task InsertPlayerSkill(PlayerSkillEntity entity);
    Task InsertPlayerStats(PlayerStatsEntity entity);

    Task<List<AllySkillEntity>> ReadAllAllySkills();
    Task<List<AllyStatsEntity>> ReadAllAllyStats();
    Task<List<BossStatsEntity>> ReadAllBossStats();
    Task<List<EnemyStatsEntity>> ReadAllEnemyStats();
    Task<List<EnemyWaveEntity>> ReadAllEnemyWaves();
    Task<List<MetadataEntity>> ReadAllMetadata();
    Task<List<PlayerShopEntity>> ReadAllPlayerShops();
    Task<List<PlayerSkillEntity>> ReadAllPlayerSkills();
    Task<List<PlayerStatsEntity>> ReadAllPlayerStats();
}

public class StubDaoImpl : IStubDao
{
    private readonly SQLiteConnection _db;

    public StubDaoImpl(SQLiteConnection db)
    {
        _db = db;
    }

    public Task InsertAllySkill(AllySkillEntity entity)
    {
        return Task.Run(() => _db.InsertOrReplace(entity));
    }

    public Task InsertAllyStats(AllyStatsEntity entity)
    {
        return Task.Run(() => _db.InsertOrReplace(entity));
    }

    public Task InsertBossStats(BossStatsEntity entity)
    {
        return Task.Run(() => _db.InsertOrReplace(entity));
    }

    public Task InsertEnemyStats(EnemyStatsEntity entity)
    {
        return Task.Run(() => _db.InsertOrReplace(entity));
    }

    public Task InsertEnemyWave(EnemyWaveEntity entity)
    {
        return Task.Run(() => _db.InsertOrReplace(entity));
    }

    public Task InsertMetadata(MetadataEntity entity)
    {
        return Task.Run(() => _db.InsertOrReplace(entity));
    }

    public async Task InsertPlayerShop(PlayerShopEntity entity)
    {
        await Task.Run(() => _db.InsertOrReplace(entity));
    }

    public Task InsertPlayerSkill(PlayerSkillEntity entity)
    {
        return Task.Run(() => _db.InsertOrReplace(entity));
    }

    public async Task InsertPlayerStats(PlayerStatsEntity entity)
    {
        await Task.Run(() => _db.InsertOrReplace(entity));
    }

    public Task<List<AllySkillEntity>> ReadAllAllySkills()
    {
        return Task.Run(() => _db.Query<AllySkillEntity>("SELECT * FROM allySkills"));
    }

    public Task<List<AllyStatsEntity>> ReadAllAllyStats()
    {
        return Task.Run(() => _db.Query<AllyStatsEntity>("SELECT * FROM allyStats"));
    }

    public Task<List<BossStatsEntity>> ReadAllBossStats()
    {
        return Task.Run(() => _db.Query<BossStatsEntity>("SELECT * FROM bossStats"));
    }

    public Task<List<EnemyStatsEntity>> ReadAllEnemyStats()
    {
        return Task.Run(() => _db.Query<EnemyStatsEntity>("SELECT * FROM enemyStats"));
    }

    public Task<List<EnemyWaveEntity>> ReadAllEnemyWaves()
    {
        return Task.Run(() => _db.Query<EnemyWaveEntity>("SELECT * FROM enemyWaves"));
    }

    public Task<List<MetadataEntity>> ReadAllMetadata()
    {
        return Task.Run(() => _db.Query<MetadataEntity>("SELECT * FROM metadata"));
    }

    public async Task<List<PlayerShopEntity>> ReadAllPlayerShops()
    {
        return await Task.Run(() => _db.Query<PlayerShopEntity>("SELECT * FROM playerShopDetails"));
    }

    public Task<List<PlayerSkillEntity>> ReadAllPlayerSkills()
    {
        return Task.Run(() => _db.Query<PlayerSkillEntity>("SELECT * FROM playerSkills"));
    }

    public Task<List<PlayerStatsEntity>> ReadAllPlayerStats()
    {
        return Task.Run(() => _db.Query<PlayerStatsEntity>("SELECT * FROM playerStats"));
    }
}
}