using System;

public interface IEnemyStats
{
    string id { get; set; }
    int totalHealth { get; set; }
    int damage { get; set; }
    int attackSpeed { get; set; }
    float movementSpeed { get; set; }
    int goldDropAmount { get; set; }
}

public class EnemyStats : IEnemyStats
{
    public string id { get; set; }
    public int totalHealth { get; set; }
    public int damage { get; set; }
    public int attackSpeed { get; set; }
    public float movementSpeed { get; set; }
    public int goldDropAmount { get; set; }

    public override bool Equals(object obj)
    {
        return obj is EnemyStats stats &&
               id == stats.id &&
               totalHealth == stats.totalHealth &&
               damage == stats.damage &&
               attackSpeed == stats.attackSpeed &&
               movementSpeed.Equals(stats.movementSpeed) && // Use Equals for float comparison
               goldDropAmount == stats.goldDropAmount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, totalHealth, damage, attackSpeed, movementSpeed, goldDropAmount);
    }

    public static bool operator == (EnemyStats left, EnemyStats right)
    {
        return Equals(left, right);
    }

    public static bool operator != (EnemyStats left, EnemyStats right)
    {
        return !Equals(left, right);
    }
}