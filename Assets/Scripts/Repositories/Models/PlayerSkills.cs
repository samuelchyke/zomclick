namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IPlayerSkills
    {
        PlayerSkill turret { get; set; }
        PlayerSkill bigBetty { get; set; }
        PlayerSkill lightningRounds { get; set; }
        PlayerSkill rallyAllies { get; set; }
        PlayerSkill incendiaryRounds { get; set; }
        PlayerSkill midasRounds { get; set; }
    }

    public record PlayerSkills : IPlayerSkills
    {
        public PlayerSkill turret { get; set; }
        public PlayerSkill bigBetty { get; set; }
        public PlayerSkill lightningRounds { get; set; }
        public PlayerSkill rallyAllies { get; set; }
        public PlayerSkill incendiaryRounds { get; set; }
        public PlayerSkill midasRounds { get; set; }
    }
}
