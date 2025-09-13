using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class PlayerSkillBuilder
    {
        public PlayerSkill buildFrom(PlayerSkillEntity entity)
        {
            return new PlayerSkill
            (
                id: entity.id,
                isUnlocked: entity.isUnlocked,
                coolDown: entity.coolDown,
                level: entity.level,
                unlockLevel: entity.unlockLevel,
                duration: entity.duration,
                buff: entity.buff,
                unlockCost: entity.unlockCost,
                upgradeCost: entity.upgradeCost,
                isActive: entity.isActive
            );
        }
    }
}