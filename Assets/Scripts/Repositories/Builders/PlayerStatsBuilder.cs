using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class PlayerStatsBuilder
    {
        public PlayerStats buildFrom(PlayerStatsEntity playerStatsEntity)
        {
            return new PlayerStats 
            (
                id: playerStatsEntity.id,
                level: playerStatsEntity.level,
                baseDamage: playerStatsEntity.baseDamage,
                critRate: playerStatsEntity.critRate,
                critMultiplier: playerStatsEntity.critMultiplier,
                totalDamage: playerStatsEntity.totalDamage,
                totalGold: playerStatsEntity.totalGold
            );
        }
    }
}