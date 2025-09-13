
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class EnemyStatsBuilder
    {
        public EnemyStats buildFrom(EnemyStatsEntity enemyEntity)
        {
            return new EnemyStats 
            (
                id: enemyEntity.id,
                totalHealth: enemyEntity.totalHealth,
                damage: enemyEntity.damage,
                attackSpeed: enemyEntity.attackSpeed,
                movementSpeed : enemyEntity.movementSpeed,
                goldDropAmount: enemyEntity.goldDropAmount
            );
        }
    }
}