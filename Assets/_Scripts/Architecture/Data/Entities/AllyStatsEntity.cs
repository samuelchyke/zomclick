using SQLite4Unity3d;

public class AllyStatsEntity
{
    [PrimaryKey]
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
}
