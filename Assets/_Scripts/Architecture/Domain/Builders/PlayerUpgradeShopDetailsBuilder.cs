public class PlayerUpgradeShopDetailsBuilder
{
    public PlayerUpgradeShopDetails ToDomain(PlayerUpgradeShopEntity shopEntity)
    {
        return new PlayerUpgradeShopDetails 
        {
            id = shopEntity.id,
            wallHealthCost = shopEntity.wallHealthCost,
            damageCost = shopEntity.damageCost,
            critDamageCost = shopEntity.critDamageCost,
            critRateCost = shopEntity.critRateCost,
        };
    }

    public PlayerUpgradeShopEntity ToEntity(PlayerUpgradeShopDetails shopStats)
    {
        return new PlayerUpgradeShopEntity 
        {
            id = shopStats.id,
            wallHealthCost = shopStats.wallHealthCost,
            damageCost = shopStats.damageCost,
            critDamageCost = shopStats.critDamageCost,
            critRateCost = shopStats.critRateCost
        };
    }
}
 