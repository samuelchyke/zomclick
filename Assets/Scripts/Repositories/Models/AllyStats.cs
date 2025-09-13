namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IAllyStats
    {
        string id { get; init; }
        string name { get; init; }
        int level { get; init; }
        int attackSpeed { get; init; }
        int baseDamage { get; init; }
        int critRate { get; init; }
        float critMultiplier { get; init; }
        int totalDamage { get; init; }
        int unlockCost { get; init; }
        int upgradeCost { get; init; }
        bool isUnlocked { get; init; }
        string lore { get; init; }
    }

    public record AllyStats(
        string id,
        string name,
        int level,
        int attackSpeed,
        int baseDamage,
        int critRate,
        float critMultiplier,
        int totalDamage,
        int unlockCost,
        int upgradeCost,
        bool isUnlocked,
        string lore
    ) : IAllyStats;
}