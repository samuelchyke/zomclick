using System;

public interface IAllyStats
{
    string id { get; set; }
    string name { get; set; }
    int attackSpeed { get; set; }
    int baseDamage { get; set; }
    int critRate { get; set; }
    float critMultiplier { get; set; }
    int totalDamage { get; set; }
    int unlockCost { get; set; }
    int upgradeCost { get; set; }
    bool isUnlocked { get; set; }
}

public class AllyStats : IAllyStats
{
    public string id { get; set; }
    public string name { get; set; }
    public int attackSpeed { get; set; }
    public int baseDamage { get; set; }
    public int critRate { get; set; }
    public float critMultiplier { get; set; }
    public int totalDamage { get; set; }
    public int unlockCost { get; set; }
    public int upgradeCost { get; set; }
    public bool isUnlocked { get; set; }

    public override bool Equals(object obj)
    {
        return obj is AllyStats stats &&
               id == stats.id &&
               name == stats.name &&
               attackSpeed == stats.attackSpeed &&
               baseDamage == stats.baseDamage &&
               critRate == stats.critRate &&
               critMultiplier.Equals(stats.critMultiplier) &&
               totalDamage == stats.totalDamage &&
               unlockCost == stats.unlockCost &&
               upgradeCost == stats.upgradeCost &&
               isUnlocked == stats.isUnlocked;
    }

    public override int GetHashCode()
    {
        int hash1 = HashCode.Combine(id, name, attackSpeed, baseDamage, critRate, critMultiplier, totalDamage, unlockCost);
        int hash2 = HashCode.Combine(upgradeCost, isUnlocked);

        return HashCode.Combine(hash1, hash2);
    }

    public static bool operator == (AllyStats left, AllyStats right)
    {
        return Equals(left, right);
    }

    public static bool operator != (AllyStats left, AllyStats right)
    {
        return !Equals(left, right);
    }
}