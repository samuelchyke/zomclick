using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed {
    [Serializable]
    public class PlayerShopDetailsDto : SeedDto
    {
        public string id;
        public int wallHealthCost;
        public int damageCost;
        public int critRateCost;
        public int critDamageCost;

        public SeedEntity toEntity()
        {
            return new PlayerShopEntity
            {
                id = id,
                wallHealthCost = wallHealthCost,
                damageCost = damageCost,
                critRateCost = critRateCost,
                critDamageCost = critDamageCost
            };
        }
    }
}