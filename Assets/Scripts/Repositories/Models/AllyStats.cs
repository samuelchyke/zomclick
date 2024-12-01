namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IAllyStats
    {
        string id { get; set; }
        string name { get; set; }
        int level { get; set; }
        int attackSpeed { get; set; }
        int baseDamage { get; set; }
        int critRate { get; set; }
        float critMultiplier { get; set; }
        int totalDamage { get; set; }
        int unlockCost { get; set; }
        int upgradeCost { get; set; }
        bool isUnlocked { get; set; }
        string lore { get; set; }
    }

    public record AllyStats : IAllyStats
    {
        public string id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int attackSpeed { get; set; }
        public int baseDamage { get; set; }
        public int critRate { get; set; }
        public float critMultiplier { get; set; }
        public int totalDamage { get; set; }
        public int unlockCost { get; set; }
        public int upgradeCost { get; set; }
        public bool isUnlocked { get; set; }
        public string lore { get; set; }
    }
}