using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class PlayerSkillsBuilder
    {
        public PlayerSkills buildFrom(
            PlayerSkillEntity turret,
            PlayerSkillEntity bigBetty,
            PlayerSkillEntity lightningRounds, 
            PlayerSkillEntity rallyAllies, 
            PlayerSkillEntity incendiaryRounds, 
            PlayerSkillEntity midasRounds
            )
        {
            return new PlayerSkills
            (
                turret: new PlayerSkillBuilder().buildFrom(turret),
                bigBetty: new PlayerSkillBuilder().buildFrom(bigBetty),
                lightningRounds: new PlayerSkillBuilder().buildFrom(lightningRounds),
                rallyAllies: new PlayerSkillBuilder().buildFrom(rallyAllies),
                incendiaryRounds: new PlayerSkillBuilder().buildFrom(incendiaryRounds),
                midasRounds: new PlayerSkillBuilder().buildFrom(midasRounds)
            );
        }
    }
}