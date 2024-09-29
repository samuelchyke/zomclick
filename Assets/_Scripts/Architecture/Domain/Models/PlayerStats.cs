using System;

public interface IPlayerStats
{
    string id { get; set; }
    int level { get; set; }
    int baseDamage { get; set; }
    int critRate { get; set; }
    float critMultiplier { get; set; }
    int totalDamage { get; set; }
    int totalGold { get; set; }
}

public record PlayerStats : IPlayerStats
{
    public string id { get; set; }
    public int level { get; set; }
    public int baseDamage { get; set; }
    public int critRate { get; set; }
    public float critMultiplier { get; set; }
    public int totalDamage { get; set; }
    public int totalGold { get; set; }

    // public override bool Equals(object obj)
    // {
    //     return obj is PlayerStats entity &&
    //            id == entity.id &&
    //            level == entity.level &&
    //            baseDamage == entity.baseDamage &&
    //            critRate == entity.critRate &&
    //            critMultiplier == entity.critMultiplier &&
    //            totalDamage == entity.totalDamage &&
    //            totalGold == entity.totalGold;
    // }

    // public override int GetHashCode()
    // {
    //     return HashCode.Combine(id, level, baseDamage, critRate, critMultiplier, totalDamage, totalGold);
    // }

    // public static bool operator == (PlayerStats left, PlayerStats right)
    // {
    //     return Equals(left, right);
    // }

    // public static bool operator != (PlayerStats left, PlayerStats right)
    // {
    //     return !Equals(left, right);
    // }
}
