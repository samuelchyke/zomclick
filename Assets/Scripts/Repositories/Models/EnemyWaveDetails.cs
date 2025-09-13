namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IEnemyWaveDetails
    {
        public string id { get; init; }
        public int round { get; init; }
        public int spawnLimit { get; init; }
        public int spawnTotal { get; init; }
        public int enemiesKilled { get; init; }
    }


    public record EnemyWaveDetails(
        string id,
        int round,
        int spawnLimit,
        int spawnTotal,
        int enemiesKilled
    ) : IEnemyWaveDetails;
}