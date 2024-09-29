using System;
public interface IBossStats
{
    string id { get; set; }
    int totalHealth { get; set; }
    int damage { get; set; }
    int attackSpeed { get; set; }
    float movementSpeed { get; set; }
    int goldDropAmount { get; set; }
}

public record BossStats : IBossStats
{
    public string id { get; set; }
    public int totalHealth { get; set; }
    public int damage { get; set; }
    public int attackSpeed { get; set; }
    public float movementSpeed { get; set; }
    public int goldDropAmount { get; set; }

    // public override bool Equals(object obj)
    // {
    //     return obj is BossStats stats &&
    //            id == stats.id &&
    //            totalHealth == stats.totalHealth &&
    //            damage == stats.damage &&
    //            attackSpeed == stats.attackSpeed &&
    //            movementSpeed.Equals(stats.movementSpeed) && // Use Equals for float comparison
    //            goldDropAmount == stats.goldDropAmount;
    // }

    // public override int GetHashCode()
    // {
    //     return HashCode.Combine(id, totalHealth, damage, attackSpeed, movementSpeed, goldDropAmount);
    // }

    // public static bool operator == (BossStats left, BossStats right)
    // {
    //     return Equals(left, right);
    // }

    // public static bool operator != (BossStats left, BossStats right)
    // {
    //     return !Equals(left, right);
    // }
}