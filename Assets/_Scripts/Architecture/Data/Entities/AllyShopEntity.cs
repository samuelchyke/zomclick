using SQLite4Unity3d;

public class AllyShopEntity
{
    [PrimaryKey]
    public string id { get; set; }
    public int totalGold { get; set; }
    public int allyUpgradeCost { get; set; }
}
