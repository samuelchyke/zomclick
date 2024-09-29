using System;

public interface IAllyStats
{
    string id { get; set; }
    string name { get; set; }
    int level { get; set; }
    int attackSpeed { get; set; }
    int baseDamage { get; set; }
    int critRate { get; set; }
    float critMultiplier { get; set; }
    int totalDamage { get; set; }
    int unlockCost { get; set; }
    int upgradeCost { get; set; }
    bool isUnlocked { get; set; }
    string lore { get; set; }
}

public record AllyStats : IAllyStats
{
    public string id { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public int attackSpeed { get; set; }
    public int baseDamage { get; set; }
    public int critRate { get; set; }
    public float critMultiplier { get; set; }
    public int totalDamage { get; set; }
    public int unlockCost { get; set; }
    public int upgradeCost { get; set; }
    public bool isUnlocked { get; set; }
    public string lore { get; set; }

    // public override bool Equals(object obj)
    // {
    //     return obj is AllyStats stats &&
    //            id == stats.id &&
    //            level == stats.level &&
    //            name == stats.name &&
    //            attackSpeed == stats.attackSpeed &&
    //            baseDamage == stats.baseDamage &&
    //            critRate == stats.critRate &&
    //            critMultiplier == stats.critMultiplier &&
    //            totalDamage == stats.totalDamage &&
    //            unlockCost == stats.unlockCost &&
    //            upgradeCost == stats.upgradeCost &&
    //            isUnlocked == stats.isUnlocked &&
    //            lore == stats.lore;
    // }

    // public override int GetHashCode()
    // {
    //     int hash1 = HashCode.Combine(id, name, level, attackSpeed, baseDamage, critRate, critMultiplier, totalDamage);
    //     int hash2 = HashCode.Combine(unlockCost, upgradeCost, isUnlocked, lore);

    //     return HashCode.Combine(hash1, hash2);
    // }

    // public static bool operator == (AllyStats left, AllyStats right)
    // {
    //     return Equals(left, right);
    // }

    // public static bool operator != (AllyStats left, AllyStats right)
    // {
    //     return !Equals(left, right);
    // }
}