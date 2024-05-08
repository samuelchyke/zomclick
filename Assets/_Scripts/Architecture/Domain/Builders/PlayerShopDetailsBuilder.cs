public class PlayerShopDetailsBuilder
{
    public PlayerShopDetails ToDomain(PlayerShopEntity shopEntity, int totalGold)
    {
        return new PlayerShopDetails 
        {
            id = shopEntity.id,
            wallHealthCost = shopEntity.wallHealthCost,
            damageCost = shopEntity.damageCost,
            critDamageCost = shopEntity.critDamageCost,
            critRateCost = shopEntity.critRateCost,
            totalGold = totalGold
        };
    }

    public PlayerShopEntity ToEntity(PlayerShopDetails shopStats)
    {
        return new PlayerShopEntity 
        {
            id = shopStats.id,
            wallHealthCost = shopStats.wallHealthCost,
            damageCost = shopStats.damageCost,
            critDamageCost = shopStats.critDamageCost,
            critRateCost = shopStats.critRateCost
        };
    }
}
 