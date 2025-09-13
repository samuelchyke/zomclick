using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class PlayerShopDetailsBuilder
    {
        public PlayerShopDetails buildFrom(PlayerShopEntity shopEntity, int totalGold)
        {
            return new PlayerShopDetails 
            (
                id: shopEntity.id,
                wallHealthCost: shopEntity.wallHealthCost,
                damageCost: shopEntity.damageCost,
                critDamageCost: shopEntity.critDamageCost,
                critRateCost:shopEntity.critRateCost,
                totalGold: totalGold
            );
        }
    }
}