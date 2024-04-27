public class BossStatsBuilder
{
    public BossStats ToDomain(BossStatsEntity bossStatsEntity)
    {
        return new BossStats 
        {
            id = bossStatsEntity.id,
            totalHealth = bossStatsEntity.totalHealth,
            damage = bossStatsEntity.damage,
            attackSpeed = bossStatsEntity.attackSpeed,
            goldDropAmount = bossStatsEntity.goldDropAmount
        };
    }

    public BossStatsEntity ToEntity(BossStats bossStats)
    {
        return new BossStatsEntity
        {
            id = bossStats.id,
            totalHealth = bossStats.totalHealth,
            damage = bossStats.damage,
            attackSpeed = bossStats.attackSpeed,
            goldDropAmount = bossStats.goldDropAmount
        };
    }
}
 