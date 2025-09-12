using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Testing {
    public static partial class Stub
    {
        public static PlayerStatsEntity PlayerStatsEntity(
            string id = null,
            int level = 1,
            int baseDamage = 1,
            int critRate = 1,
            float critMultiplier = 1.0f,
            int totalDamage = 1,
            int totalGold = 1,
            int totalRelics  = 1
        )
        {
            return new PlayerStatsEntity
            {
                id = id ?? Guid.NewGuid().ToString(),
                level = level,
                baseDamage = baseDamage,
                critRate = critRate,
                critMultiplier = critMultiplier,
                totalDamage = totalDamage,
                totalGold = totalGold,
                totalRelics  = totalRelics
            };
        }
    }
}