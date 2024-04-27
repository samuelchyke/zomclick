using System;

public interface IPlayerUpgradeShopDetails
{
    string id { get; set; }
    int wallHealthCost { get; set; }
    int damageCost { get; set; }
    int critDamageCost { get; set; }
    int critRateCost { get; set; }
}

public class PlayerUpgradeShopDetails : IPlayerUpgradeShopDetails
{
    public string id { get; set; }
    public int wallHealthCost { get; set; }
    public int damageCost { get; set; }
    public int critDamageCost { get; set; }
    public int critRateCost { get; set; }

    public override bool Equals(object obj)
    {
        return obj is PlayerUpgradeShopDetails entity &&
               id == entity.id &&
               wallHealthCost == entity.wallHealthCost &&
               damageCost == entity.damageCost &&
               critDamageCost == entity.critDamageCost &&
               critRateCost == entity.critRateCost;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, wallHealthCost, damageCost, critDamageCost, critRateCost);
    }

    public static bool operator == (PlayerUpgradeShopDetails left, PlayerUpgradeShopDetails right)
    {
        return Equals(left, right);
    }

    public static bool operator != (PlayerUpgradeShopDetails left, PlayerUpgradeShopDetails right)
    {
        return !Equals(left, right);
    }
}
