using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(EnemyWaveEntity.TableName)]
    public record EnemyWaveEntity : SeedEntity
    {
        public const string TableName = "enemyWaves";

        [PrimaryKey]
        public string id { get; set; }
        public int round { get; set; }
        public int spawnLimit { get; set; }
        public int spawnTotal { get; set; }
        public int enemiesKilled { get; set; }

        public EnemyWaveEntity()
        {
            tableName = TableName;
        }

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}