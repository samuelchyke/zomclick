using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;

public class AllyShopEntity : SeedEntity
{
    [PrimaryKey]
    public string id { get; set; }
    public int totalGold { get; set; }
    public int allyUpgradeCost { get; set; }

    public AllyShopEntity()
    {
        tableName = "allyShopDetails";
    }

    public override List<PropertyInfo> Keys => new List<PropertyInfo>
    {
        GetType().GetProperty(nameof(id))
    };
}
