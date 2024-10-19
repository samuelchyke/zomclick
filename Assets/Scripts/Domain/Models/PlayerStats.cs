namespace Com.Studio.Zomclick.Assets.Scripts.Domain.Models {
    public interface IPlayerStats
    {
        string id { get; set; }
        int level { get; set; }
        int baseDamage { get; set; }
        int critRate { get; set; }
        float critMultiplier { get; set; }
        int totalDamage { get; set; }
        int totalGold { get; set; }
    }

    public record PlayerStats : IPlayerStats
    {
        public string id { get; set; }
        public int level { get; set; }
        public int baseDamage { get; set; }
        public int critRate { get; set; }
        public float critMultiplier { get; set; }
        public int totalDamage { get; set; }
        public int totalGold { get; set; }
    }
}