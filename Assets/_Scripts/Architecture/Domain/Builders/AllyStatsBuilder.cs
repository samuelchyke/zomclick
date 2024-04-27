public class AllyStatsBuilder
{
    public AllyStats ToDomain(AllyStatsEntity entity)
    {
        return new AllyStats
        {
            id = entity.id,
            name = entity.name,
            attackSpeed = entity.attackSpeed,
            baseDamage = entity.baseDamage,
            critRate = entity.critRate,
            critMultiplier = entity.critMultiplier,
            totalDamage = entity.totalDamage,
            unlockCost = entity.unlockCost,
            upgradeCost = entity.upgradeCost,
            isUnlocked = entity.isUnlocked
        };
    }

    public AllyStatsEntity ToEntity(AllyStats allyStats)
    {
        return new AllyStatsEntity
        {
            id = allyStats.id,
            name = allyStats.name,
            attackSpeed = allyStats.attackSpeed,
            baseDamage = allyStats.baseDamage,
            critRate = allyStats.critRate,
            critMultiplier = allyStats.critMultiplier,
            totalDamage = allyStats.totalDamage,
            unlockCost = allyStats.unlockCost,
            upgradeCost = allyStats.upgradeCost,
            isUnlocked = allyStats.isUnlocked
        };
    }
}