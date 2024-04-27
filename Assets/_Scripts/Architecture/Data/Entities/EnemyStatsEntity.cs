using SQLite4Unity3d;

public class EnemyStatsEntity
{
    [PrimaryKey]
    public string id { get; set; }
    public int totalHealth { get; set; }
    public int damage { get; set; }
    public int attackSpeed { get; set; }
    public float movementSpeed { get; set; }
    public int goldDropAmount { get; set; }
}
