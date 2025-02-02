using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dto {
    [Serializable]
    public class ArtifactShopDto : SeedDto
    {
        public string id;
        public int artifactUnlockCost;

        public SeedEntity toEntity()
        {
            return new ArtifactShopEntity
            {
                id = id,
                artifactUnlockCost = artifactUnlockCost,
            };
        }
    }
}