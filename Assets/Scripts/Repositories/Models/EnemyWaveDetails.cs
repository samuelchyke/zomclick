namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IEnemyWaveDetails
    {
        public string id { get; set; }
        public int round { get; set; }
        public int spawnLimit { get; set; }
        public int spawnTotal { get; set; }
        public int enemiesKilled { get; set; }
    }


    public record EnemyWaveDetails : IEnemyWaveDetails
    {
        public string id { get; set; }
        public int round { get; set; }
        public int spawnLimit { get; set; }
        public int spawnTotal { get; set; }
        public int enemiesKilled { get; set; }
    }
}