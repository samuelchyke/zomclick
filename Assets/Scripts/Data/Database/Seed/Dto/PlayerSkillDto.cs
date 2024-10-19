using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed {
    [Serializable]
    public class PlayerSkillDto : SeedDto
    {
        public string id;
        public bool isUnlocked;
        public int unlockLevel;
        public int level;
        public int duration;
        public int coolDown;
        public int buff;
        public int unlockCost;
        public int upgradeCost;
        public bool isActive;

        public SeedEntity toEntity()
        {
            return new PlayerSkillEntity
            {
                id = id,
                isUnlocked = isUnlocked,
                unlockLevel = unlockLevel,
                level = level,
                duration = duration,
                coolDown = coolDown,
                buff = buff,
                unlockCost = unlockCost,
                upgradeCost = upgradeCost,
                isActive = isActive
            };
        }
    }
}