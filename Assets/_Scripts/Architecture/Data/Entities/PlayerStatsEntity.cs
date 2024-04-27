using SQLite4Unity3d;

public class PlayerStatsEntity
{
    [PrimaryKey]
    public string id { get; set; }
    public int wallHealth { get; set; }
    public int baseDamage { get; set; }
    public int critRate { get; set; }
    public float critMultiplier { get; set; }
    public int totalDamage { get; set; }
    public int totalGold { get; set; }
}
