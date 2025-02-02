using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class ArtifactBuilder
    {
        public Artifact ToDomain(ArtifactEntity entity)
        {
            return new Artifact
            {
                id = entity.id,
                name = entity.name,
                level = entity.level,
                buff = entity.buff,
                upgradeDetails = entity.upgradeDetails,
                upgradeCost = entity.upgradeCost,
                description = entity.description,
                isUnlocked = entity.isUnlocked
            };
        }

        public ArtifactEntity ToEntity(Artifact artifact)
        {
            return new ArtifactEntity
            {
                id = artifact.id,
                name = artifact.name,
                level = artifact.level,
                buff = artifact.buff,
                upgradeDetails = artifact.upgradeDetails,
                upgradeCost = artifact.upgradeCost,
                description = artifact.description
            };
        }
    }
}