namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IAtrifact
    {
        public string id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public float buff { get; set; }
        public float upgradeDetails { get; set; }
        public int unlockCost { get; set; }
        public int upgradeCost { get; set; }
        public string description { get; set; }
        public bool isUnlocked { get; set; }
    }

    public record Artifact : IAtrifact
    {
        public string id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public float buff { get; set; }
        public float upgradeDetails { get; set; }
        public int unlockCost { get; set; }
        public int upgradeCost { get; set; }
        public string description { get; set; }
        public bool isUnlocked { get; set; }
    }
}