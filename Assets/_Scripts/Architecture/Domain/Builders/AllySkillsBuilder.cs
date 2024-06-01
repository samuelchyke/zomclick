public class AllySkillsBuilder
{
    public AllySkill ToDomain(AllySkillsEntity entity)
    {
        return new AllySkill
        {
            id = entity.id,
            allyId = entity.allyId,
            isUnlocked = entity.isUnlocked, 
            description = entity.description,
            unlockLevel = entity.unlockLevel,
            buff = entity.buff
        };
    }

    public AllySkillsEntity ToEntity(AllySkill skill)
    {
        return new AllySkillsEntity
        {
            id = skill.id,
            allyId = skill.allyId,
            isUnlocked = skill.isUnlocked,
            description = skill.description,
            unlockLevel = skill.unlockLevel,
            buff = skill.buff
        };
    }
}