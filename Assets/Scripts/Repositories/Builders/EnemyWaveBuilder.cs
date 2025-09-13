using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class EnemyWaveDetailsBuilder
    {
        public EnemyWaveDetails buildFrom(EnemyWaveEntity entity)
        {
            return new EnemyWaveDetails
            (
                id: entity.id,
                round: entity.round,
                spawnLimit: entity.spawnLimit,
                spawnTotal: entity.spawnTotal,
                enemiesKilled: entity.enemiesKilled
            );
        }
    }
}