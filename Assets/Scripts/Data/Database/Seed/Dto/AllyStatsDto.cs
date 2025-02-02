using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dto {
    [Serializable]
    public class AllyStatsDto : SeedDto
    {
        public string id;
        public string name;
        public int level;
        public int attackSpeed;
        public int baseDamage;
        public int critRate;
        public float critMultiplier;
        public int totalDamage;
        public int unlockCost;
        public int upgradeCost;
        public bool isUnlocked;
        public string lore;

        public SeedEntity toEntity()
        {
            return new AllyStatsEntity
            {
                id = id,
                name = name,
                level = level,  
                attackSpeed = attackSpeed,
                baseDamage = baseDamage,
                critRate = critRate,
                critMultiplier = critMultiplier,
                totalDamage = totalDamage,
                unlockCost = unlockCost,
                upgradeCost = upgradeCost,
                isUnlocked = isUnlocked,
                lore = lore
            };
        }
    }
}
