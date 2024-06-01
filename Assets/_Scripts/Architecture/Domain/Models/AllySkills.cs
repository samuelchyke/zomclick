using System;

public interface IAllySkill
{
    string id { get; set; }
    string allyId { get; set; }
    bool isUnlocked { get; set; }
    string description { get; set; }
    int unlockLevel { get; set; }
    int buff { get; set; }
}

public class AllySkill : IAllySkill
{
    public string id { get; set; }
    public string allyId { get; set; }
    public bool isUnlocked { get; set; }
    public string description { get; set; }
    public int unlockLevel { get; set; }
    public int buff { get; set; }

    public override bool Equals(object obj)
    {
        return obj is AllySkill skill &&
               id == skill.id &&
               allyId == skill.allyId &&
               isUnlocked == skill.isUnlocked &&
               description == skill.description &&
               unlockLevel == skill.unlockLevel &&
               buff == skill.buff;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(id, allyId, isUnlocked, description, unlockLevel, buff);
    }

    public static bool operator == (AllySkill left, AllySkill right)
    {
        return Equals(left, right);
    }

    public static bool operator != (AllySkill left, AllySkill right)
    {
        return !Equals(left, right);
    }
}