using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Testing {
    public static class StubPlayerShopEntity { 
        public static PlayerShopEntity PlayerShopEntity(this Stub stub,
            string id = null,
            int wallHealthCost = 100,
            int damageCost = 50,
            int critDamageCost = 75,
            int critRateCost = 25
        )   
        {
            return new PlayerShopEntity
            {
                id = id ?? Guid.NewGuid().ToString(),
                wallHealthCost = wallHealthCost,
                damageCost = damageCost,
                critDamageCost = critDamageCost,
                critRateCost = critRateCost
            };
        }
    }
}