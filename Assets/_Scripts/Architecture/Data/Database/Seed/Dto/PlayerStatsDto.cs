using System;

[Serializable]
public class PlayerStatsDto
{
    public string id { get; set; }
    public int level { get; set; }
    public int baseDamage { get; set; }
    public int critRate { get; set; }
    public float critMultiplier { get; set; }
    public int totalDamage { get; set; }
    public int totalGold { get; set; }
}
