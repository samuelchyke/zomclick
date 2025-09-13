namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IPlayerStats
    {
        string id { get; init; }
        int level { get; init; }
        int baseDamage { get; init; }
        int critRate { get; init; }
        float critMultiplier { get; init; }
        int totalDamage { get; init; }
        int totalGold { get; init; }
    }

    public record PlayerStats 
    (
        string id,
        int level,
        int baseDamage,
        int critRate,
        float critMultiplier,
        int totalDamage,
        int totalGold
     ) : IPlayerStats;
}