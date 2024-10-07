using System;

[Serializable]
public class EnemyWaveDto
{
    public string id { get; set; }
    public int round { get; set; }
    public int spawnLimit { get; set; }
    public int spawnTotal { get; set; }
    public int enemiesKilled { get; set; }
}
