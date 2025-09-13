namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IPlayerSkills
    {
        PlayerSkill turret { get; init; }
        PlayerSkill bigBetty { get; init; }
        PlayerSkill lightningRounds { get; init; }
        PlayerSkill rallyAllies { get; init; }
        PlayerSkill incendiaryRounds { get; init; }
        PlayerSkill midasRounds { get; init; }
    }

    public record PlayerSkills(
        PlayerSkill turret,
        PlayerSkill bigBetty,
        PlayerSkill lightningRounds,
        PlayerSkill rallyAllies,
        PlayerSkill incendiaryRounds,
        PlayerSkill midasRounds
    ) : IPlayerSkills;
}
