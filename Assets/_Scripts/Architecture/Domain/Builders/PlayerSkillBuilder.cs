public class PlayerSkillBuilder
{
    public PlayerSkill ToDomain(PlayerSkillEntity entity)
    {
        return new PlayerSkill
        {
            id = entity.id,
            isUnlocked = entity.isUnlocked,
            coolDown = entity.coolDown,
            level = entity.level,
            unlockLevel = entity.unlockLevel,
            duration = entity.duration,
            buff = entity.buff,
            unlockCost = entity.unlockCost,
            upgradeCost = entity.upgradeCost
        };
    }

    public PlayerSkillEntity ToEntity(PlayerSkill skill)
    {
        return new PlayerSkillEntity
        {
            id = skill.id,
            isUnlocked = skill.isUnlocked,
            coolDown = skill.coolDown,
            level = skill.level,
            unlockLevel = skill.unlockLevel,
            duration = skill.duration,
            buff = skill.buff,
            unlockCost = skill.unlockCost,
            upgradeCost = skill.upgradeCost
        };
    }
}