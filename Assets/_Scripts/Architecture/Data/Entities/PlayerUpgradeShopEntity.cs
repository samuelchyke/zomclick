using SQLite4Unity3d;

public class PlayerUpgradeShopEntity
{
    [PrimaryKey]
    public string id { get; set; }
    public int wallHealthCost { get; set; }
    public int damageCost { get; set; }
    public int critDamageCost { get; set; }
    public int critRateCost { get; set; }
}
