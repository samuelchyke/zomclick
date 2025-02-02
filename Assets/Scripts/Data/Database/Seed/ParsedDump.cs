using System;
using System.Collections.Generic;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dto;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed {
    [Serializable]
    public class ParsedDataDump {
        public MetadataDto metadata;
        public ParsedDump data;
    }

    [Serializable]
    public class ParsedDump {   
        public PlayerStatsDto playerStats;
        public List<PlayerSkillDto> playerSkills;
        public PlayerShopDto playerShop;
        public ArtifactShopDto artifactShop;
        public List<ArtifactDto> artifacts;
        public EnemyStatsDto enemyStats;
        public EnemyWaveDto enemyWaves;
        public BossStatsDto bossStats;
        public List<AllyStatsDto> allyStats;
        public List<AllySkillDto> allySkills;
    }
}