using System;
using System.Collections.Generic;

[Serializable]
public class ParsedDataDump {
    public MetadataDto metadata;
    public ParsedDump data;
}

[Serializable]
public class ParsedDump {   
    public PlayerStatsDto playerStats;
    public List<PlayerSkillDto> playerSkills;
    public PlayerShopDetailsDto playerShopDetails;
    public EnemyStatsDto enemyStats;
    public EnemyWaveDto enemyWaves;
    public BossStatsDto bossStats;
    public List<AllyStatsDto> allyStats;
    public List<AllySkillDto> allySkills;
}