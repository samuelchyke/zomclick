using System;

[Serializable]
public class AllySkillDto
{
    public string id { get; set; }
    public string allyId { get; set; }
    public bool isUnlocked { get; set; }
    public string description { get; set; }
    public int unlockLevel { get; set; }
    public int buff { get; set; }
}