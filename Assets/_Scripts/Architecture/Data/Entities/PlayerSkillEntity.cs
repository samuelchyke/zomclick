using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("Style", "IDE0002:Simplify name")]
[Table(PlayerSkillEntity.TableName)]
public class PlayerSkillEntity : SeedEntity
{
    public const string TableName = "playerSkills";

    [PrimaryKey]
    public string id { get; set; }
    public bool isUnlocked { get; set; }
    public int unlockLevel { get; set; }
    public int level { get; set; }
    public int duration { get; set; }
    public int coolDown { get; set; }
    public int buff { get; set; }
    public int unlockCost { get; set; }
    public int upgradeCost { get; set; }
    public bool isActive { get; set; }

    public PlayerSkillEntity()
    {
        tableName = TableName;
    }

    public override List<PropertyInfo> Keys => new List<PropertyInfo>
    {
        GetType().GetProperty(nameof(id))
    };
}