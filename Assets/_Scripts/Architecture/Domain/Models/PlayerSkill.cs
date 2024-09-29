using System;

public interface IPlayerSkill
{
    string id { get; set; }
    bool isUnlocked { get; set; }
    int coolDown { get; set; }
    int level { get; set; }
    int unlockLevel { get; set; }
    int duration { get; set; }
    int buff { get; set; }
    int unlockCost { get; set; }
    int upgradeCost { get; set; }
    bool isActive { get; set; }
}

public record PlayerSkill : IPlayerSkill
{
    public string id { get; set; }
    public bool isUnlocked { get; set; }
    public int coolDown { get; set; }
    public int level { get; set; }
    public int unlockLevel { get; set; }
    public int duration { get; set; }
    public int buff { get; set; }
    public int unlockCost { get; set; }
    public int upgradeCost { get; set; }
    public bool isActive { get; set; }
}