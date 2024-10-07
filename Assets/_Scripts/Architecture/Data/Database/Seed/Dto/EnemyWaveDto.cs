using System;

[Serializable]
public class EnemyWaveDto : SeedDto
{
    public string id;
    public int round;
    public int spawnLimit;
    public int spawnTotal;
    public int enemiesKilled;

    public SeedEntity toEntity()    
    {
        return new EnemyWaveEntity
        {
            id = id,
            round = round,
            spawnLimit = spawnLimit,
            spawnTotal = spawnTotal,
            enemiesKilled = enemiesKilled
        };
    }
}
