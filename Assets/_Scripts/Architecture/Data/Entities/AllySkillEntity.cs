using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;

public class AllySkillEntity : SeedEntity
{
    public const string TableName = "allySkills";

    [PrimaryKey]
    public string id { get; set; }
    public string allyId { get; set; }
    public bool isUnlocked { get; set; }
    public string description { get; set; }
    public int unlockLevel { get; set; }
    public int buff { get; set; }

    public AllySkillEntity()
    {
        tableName = TableName;
    }

    public override List<PropertyInfo> Keys => new List<PropertyInfo>
    {
        GetType().GetProperty(nameof(id))
    };
}
