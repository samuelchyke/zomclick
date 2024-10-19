using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.Builders {
    public class PlayerStatsBuilder
    {
        public PlayerStats ToDomain(PlayerStatsEntity playerStatsEntity)
        {
            return new PlayerStats 
            {
                id = playerStatsEntity.id,
                level = playerStatsEntity.level,
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
                level = playerStats.level,
                baseDamage = playerStats.baseDamage,
                critRate = playerStats.critRate,
                critMultiplier = playerStats.critMultiplier,
                totalDamage = playerStats.totalDamage
            };
        }
    }
}