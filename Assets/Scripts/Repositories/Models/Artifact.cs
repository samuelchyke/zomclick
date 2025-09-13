namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IAtrifact
    {
        public string id { get; init; }
        public string name { get; init; }
        public int level { get; init; }
        public float buff { get; init; }
        public float upgradeDetails { get; init; }
        // public int unlockCost { get; init; }
        public int upgradeCost { get; init; }
        public string description { get; init; }
        public bool isUnlocked { get; init; }
    }

    public record Artifact
    (
        string id,
        string name,
        int level,
        float buff,
        float upgradeDetails,
        // int unlockCost,
        int upgradeCost,
        string description,
        bool isUnlocked
    ) : IAtrifact;
}