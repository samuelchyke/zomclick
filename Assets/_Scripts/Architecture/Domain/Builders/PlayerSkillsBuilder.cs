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
        {
            turret = new PlayerSkillBuilder().ToDomain(turret),
            bigBetty = new PlayerSkillBuilder().ToDomain(bigBetty),
            lightningRounds = new PlayerSkillBuilder().ToDomain(lightningRounds),
            rallyAllies = new PlayerSkillBuilder().ToDomain(rallyAllies),
            incendiaryRounds = new PlayerSkillBuilder().ToDomain(incendiaryRounds),
            midasRounds = new PlayerSkillBuilder().ToDomain(midasRounds)
        };
    }
}