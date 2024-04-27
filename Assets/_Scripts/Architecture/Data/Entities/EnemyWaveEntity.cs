using SQLite4Unity3d;

public class EnemyWaveEntity
{
    [PrimaryKey]
    public string id { get; set; }
    public int round { get; set; }
    public int spawnLimit { get; set; }
    public int spawnTotal { get; set; }
    public int enemiesKilled { get; set; }
}
