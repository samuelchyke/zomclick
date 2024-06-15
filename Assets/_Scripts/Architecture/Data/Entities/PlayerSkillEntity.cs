using SQLite4Unity3d;

public class PlayerSkillEntity
{
    [PrimaryKey]
    public string id { get; set; }
    public bool isUnlocked { get; set; }
    public int coolDown { get; set; }
    public int level { get; set; }
    public int unlockLevel { get; set; }
    public int duration { get; set; }
    public int buff { get; set; }
    public int unlockCost { get; set; }
    public int upgradeCost { get; set; }
}