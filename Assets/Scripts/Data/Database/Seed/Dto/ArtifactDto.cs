using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dto {
    [Serializable]
    public class ArtifactDto : SeedDto
    {
        public string id;
        public string name;
        public int level;
        public float buff;
        public float upgradeDetails;
        public int upgradeCost;
        public string description;
        public bool isUnlocked;

        public SeedEntity toEntity()
        {
            return new ArtifactEntity
            {
                id = id,
                name = name,
                level = level,
                buff = buff,
                upgradeDetails = upgradeDetails,
                upgradeCost = upgradeCost,
                description = description,
                isUnlocked = isUnlocked
            };
        }
    }
}