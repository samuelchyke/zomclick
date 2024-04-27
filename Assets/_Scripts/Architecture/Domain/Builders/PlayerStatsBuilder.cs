public class PlayerStatsBuilder
{
    public PlayerStats ToDomain(PlayerStatsEntity playerStatsEntity)
    {
        return new PlayerStats 
        {
            id = playerStatsEntity.id,
            wallHealth = playerStatsEntity.wallHealth,
            baseDamage = playerStatsEntity.baseDamage,
            critRate = playerStatsEntity.critRate,
            critMultiplier = playerStatsEntity.critMultiplier,
            totalDamage = playerStatsEntity.totalDamage,
            totalGold = playerStatsEntity.totalGold
        };
    }

    public PlayerStatsEntity ToEntity(PlayerStats playerStats)
    {
        return new PlayerStatsEntity
        {
            id = playerStats.id,
            totalGold = playerStats.totalGold,
            wallHealth = playerStats.wallHealth,
            baseDamage = playerStats.baseDamage,
            critRate = playerStats.critRate,
            critMultiplier = playerStats.critMultiplier,
            totalDamage = playerStats.totalDamage
        };
    }
}
 