using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;

public class AllyStatsEntity : SeedEntity
{
    public const string TableName = "allyStats";

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

    public AllyStatsEntity()
    {
        tableName = TableName;
    }

    public override List<PropertyInfo> Keys => new List<PropertyInfo>
    {
        GetType().GetProperty(nameof(id))
    };
}
