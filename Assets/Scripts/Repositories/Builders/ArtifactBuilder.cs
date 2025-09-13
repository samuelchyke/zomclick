using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class ArtifactBuilder
    {
        public Artifact buildFrom(ArtifactEntity entity)
        {
            return new Artifact
            (
                id: entity.id,
                name: entity.name,
                level: entity.level,
                buff: entity.buff,
                upgradeDetails: entity.upgradeDetails,
                upgradeCost: entity.upgradeCost,
                description: entity.description,
                isUnlocked: entity.isUnlocked
            );
        }
    }
}