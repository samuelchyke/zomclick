using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dto {
    [Serializable]
    public class AllySkillDto : SeedDto
    {
        public string id;
        public string allyId;
        public bool isUnlocked;
        public string description;
        public int unlockLevel;
        public int buff;

        public SeedEntity toEntity()
        {
            return new AllySkillEntity {
                id = id,
                allyId = allyId,
                isUnlocked = isUnlocked,
                description = description,
                unlockLevel = unlockLevel,
                buff = buff
            };
        }
    }
}