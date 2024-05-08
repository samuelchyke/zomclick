using System;

public interface IPlayerShopDetails
{
    string id { get; set; }
    int wallHealthCost { get; set; }
    int damageCost { get; set; }
    int critDamageCost { get; set; }
    int critRateCost { get; set; }
    int totalGold { get; set; }
}

public class PlayerShopDetails : IPlayerShopDetails
{
    public string id { get; set; }
    public int wallHealthCost { get; set; }
    public int damageCost { get; set; }
    public int critDamageCost { get; set; }
    public int critRateCost { get; set; }
    public int totalGold { get; set; }

    public override bool Equals(object obj)
    {
        return obj is PlayerShopDetails entity &&
               id == entity.id &&
               wallHealthCost == entity.wallHealthCost &&
               damageCost == entity.damageCost &&
               critDamageCost == entity.critDamageCost &&
               critRateCost == entity.critRateCost &&
               totalGold == entity.totalGold;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, wallHealthCost, damageCost, critDamageCost, critRateCost, totalGold);
    }

    public static bool operator == (PlayerShopDetails left, PlayerShopDetails right)
    {
        return Equals(left, right);
    }

    public static bool operator != (PlayerShopDetails left, PlayerShopDetails right)
    {
        return !Equals(left, right);
    }
}
