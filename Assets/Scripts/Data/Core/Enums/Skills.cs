namespace Com.Studio.Zomclick.Assets.Scripts.Data.Core.Enums {
    public enum Skill
    {
        BigBetty,
        Turret,
        LightningRounds,
        RallyAllies,
        IncendiaryRounds,
        MidasRounds
    }

    public static class SkillExtensions
    {
        public static string id(this Skill skill)
        {
            return skill switch
            {
                Skill.BigBetty => "big_betty_id",
                Skill.Turret => "turret_id",
                Skill.LightningRounds => "lightning_rounds_id",
                Skill.RallyAllies => "rally_allies_id",
                Skill.IncendiaryRounds => "incendiary_rounds_id",
                Skill.MidasRounds => "midas_rounds_id",
            };
        }
    }
}