using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;

public class AllyShopEntity : SeedEntity
{
    public const string TableName = "allyShopDetails";

    [PrimaryKey]
    public string id { get; set; }
    public int totalGold { get; set; }
    public int allyUpgradeCost { get; set; }

    public AllyShopEntity()
    {
        tableName = TableName;
    }

    public override List<PropertyInfo> Keys => new List<PropertyInfo>
    {
        GetType().GetProperty(nameof(id))
    };
}
