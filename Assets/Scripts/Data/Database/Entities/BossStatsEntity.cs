using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(BossStatsEntity.TableName)]
    public class BossStatsEntity : SeedEntity
    {
        public const string TableName = "bossStats";

        [PrimaryKey]
        public string id { get; set; }
        public int totalHealth { get; set; }
        public int damage { get; set; }
        public int attackSpeed { get; set; }
        public float movementSpeed { get; set; }
        public int goldDropAmount { get; set; }

        public BossStatsEntity()
        {
            tableName = TableName;
        }

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}