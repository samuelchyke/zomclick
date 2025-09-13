namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IEnemyStats
    {
        string id { get; init; }
        int totalHealth { get; init; }
        int damage { get; init; }
        int attackSpeed { get; init; }
        float movementSpeed { get; init; }
        int goldDropAmount { get; init; }
    }

    public record EnemyStats(
        string id,
        int totalHealth,
        int damage,
        int attackSpeed,
        float movementSpeed,
        int goldDropAmount
    ) : IEnemyStats;
}