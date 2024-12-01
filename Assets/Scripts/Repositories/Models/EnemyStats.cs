namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IEnemyStats
    {
        string id { get; set; }
        int totalHealth { get; set; }
        int damage { get; set; }
        int attackSpeed { get; set; }
        float movementSpeed { get; set; }
        int goldDropAmount { get; set; }
    }

    public record EnemyStats : IEnemyStats
    {
        public string id { get; set; }
        public int totalHealth { get; set; }
        public int damage { get; set; }
        public int attackSpeed { get; set; }
        public float movementSpeed { get; set; }
        public int goldDropAmount { get; set; }
    }
}