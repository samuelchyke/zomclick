using System;

[Serializable]
public class PlayerStatsDto : SeedDto
{
    public string id;
    public int level;
    public int baseDamage;
    public int critRate;
    public float critMultiplier;
    public int totalDamage;
    public int totalGold;

    public SeedEntity toEntity()
    {
        return new PlayerStatsEntity
        {
            id = id,
            level = level,
            baseDamage = baseDamage,
            critRate = critRate,
            critMultiplier = critMultiplier,
            totalDamage = totalDamage,
            totalGold = totalGold
        };
    }
}
