using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class BossStatsBuilder
    {
        public BossStats buildFrom(BossStatsEntity bossStatsEntity)
        {
            return new BossStats 
            (
                id: bossStatsEntity.id,
                totalHealth: bossStatsEntity.totalHealth,
                damage: bossStatsEntity.damage,
                attackSpeed: bossStatsEntity.attackSpeed,
                movementSpeed: bossStatsEntity.movementSpeed,
                goldDropAmount: bossStatsEntity.goldDropAmount
            );
        }
    }
}