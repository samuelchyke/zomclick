using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class EnemyWaveDetailsBuilder
    {
        public EnemyWaveDetails ToDomain(EnemyWaveEntity entity)
        {
            return new EnemyWaveDetails
            {
                id = entity.id,
                round = entity.round,
                spawnLimit = entity.spawnLimit,
                spawnTotal = entity.spawnTotal,
                enemiesKilled = entity.enemiesKilled
            };
        }

        public EnemyWaveEntity ToEntity(EnemyWaveDetails details)
        {
            return new EnemyWaveEntity
            {
                id = details.id,
                round = details.round,
                spawnLimit = details.spawnLimit,
                spawnTotal = details.spawnTotal,
                enemiesKilled = details.enemiesKilled
            };
        }
    }
}