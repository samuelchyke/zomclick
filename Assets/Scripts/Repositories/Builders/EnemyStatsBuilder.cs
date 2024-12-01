
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class EnemyStatsBuilder
    {
        public EnemyStats ToDomain(EnemyStatsEntity enemyEntity)
        {
            return new EnemyStats 
            {
                id = enemyEntity.id,
                totalHealth = enemyEntity.totalHealth,
                damage = enemyEntity.damage,
                attackSpeed = enemyEntity.attackSpeed,
                goldDropAmount = enemyEntity.goldDropAmount
            };
        }

        public EnemyStatsEntity ToEntity(EnemyStats enemyStats)
        {
            return new EnemyStatsEntity
            {
                id = enemyStats.id,
                totalHealth = enemyStats.totalHealth,
                damage = enemyStats.damage,
                attackSpeed = enemyStats.attackSpeed,
                goldDropAmount = enemyStats.goldDropAmount
            };
        }
    }
}