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
}

public class PlayerSkill : IPlayerSkill
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

    public override bool Equals(object obj)
    {
        return obj is PlayerSkill skill &&
               id == skill.id &&
               isUnlocked == skill.isUnlocked &&
               coolDown == skill.coolDown &&
               level == skill.level &&
               unlockLevel == skill.unlockLevel &&
               duration == skill.duration &&
               buff == skill.buff &&
               unlockCost == skill.unlockCost &&
               upgradeCost == skill.upgradeCost;
    }

    public override int GetHashCode()
    {
        int hash1 = HashCode.Combine(id, isUnlocked, coolDown, level, unlockLevel, duration, buff, unlockCost);
        int hash2 = HashCode.Combine(upgradeCost);

        return HashCode.Combine(hash1 + hash2);
    }

    public static bool operator == (PlayerSkill left, PlayerSkill right)
    {
        return Equals(left, right);
    }

    public static bool operator != (PlayerSkill left, PlayerSkill right)
    {
        return !Equals(left, right);
    }
}