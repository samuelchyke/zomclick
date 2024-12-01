using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class AllySkillsBuilder
    {
        public AllySkill ToDomain(AllySkillEntity entity)
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

        public AllySkillEntity ToEntity(AllySkill skill)
        {
            return new AllySkillEntity
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
}