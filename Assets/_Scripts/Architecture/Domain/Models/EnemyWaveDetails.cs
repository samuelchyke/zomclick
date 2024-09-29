using System;

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

    // public override bool Equals(object obj)
    // {
    //     return obj is EnemyWaveDetails details &&
    //            id == details.id &&
    //            round == details.round &&
    //            spawnLimit == details.spawnLimit &&
    //            spawnTotal == details.spawnTotal &&
    //            enemiesKilled == details.enemiesKilled;
    // }

    // public override int GetHashCode()
    // {
    //     return HashCode.Combine(id, round, spawnLimit, spawnTotal, enemiesKilled);
    // }

    // public static bool operator == (EnemyWaveDetails left, EnemyWaveDetails right)
    // {
    //     return Equals(left, right);
    // }

    // public static bool operator != (EnemyWaveDetails left, EnemyWaveDetails right)
    // {
    //     return !Equals(left, right);
    // }
}
