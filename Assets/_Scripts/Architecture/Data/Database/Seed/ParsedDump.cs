using System;
using System.Collections.Generic;

[Serializable]
public class ParsedDataDump {

    // [SerialName(MetadataEntity.TableName)]
    public MetadataDto metadata { get; set; }
    public ParsedDump data { get; set; }
}

[Serializable]
public class ParsedDump {   

    // [SerialName(PlayerStatsEntity.TableName)]
    public PlayerStatsDto playerStats { get; set; }

    // [SerialName(PlayerSkillEntity.TableName)]
    public List<PlayerSkillDto> playerSkills { get; set; }

    // [SerialName(PlayerShopEntity.TableName)]
    public PlayerShopDto shopDetails { get; set; }

    // [SerialName(EnemyStatsEntity.TableName)]
    public EnemyStatsDto enemyStats { get; set; }

    // [SerialName(EnemyWaveEntity.TableName)]
    public EnemyWaveDto enemyWave { get; set; }

    // [SerialName(BossStatsEntity.TableName)]
    public BossStatsDto bossStats { get; set; }

    // [SerialName(AllyStatsEntity.TableName)]
    public List<AllyStatsDto> allyStats { get; set; }

    // [SerialName(AllySkillEntity.TableName)]
    public List<AllySkillDto> allySkills { get; set; }
}