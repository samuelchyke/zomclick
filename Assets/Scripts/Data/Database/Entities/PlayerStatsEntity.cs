using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(PlayerStatsEntity.TableName)]
    public class PlayerStatsEntity : SeedEntity
    {
        public const string TableName = "playerStats";

        [PrimaryKey]
        public string id { get; set; }
        public int level { get; set; }
        public int baseDamage { get; set; }
        public int critRate { get; set; }
        public float critMultiplier { get; set; }
        public int totalDamage { get; set; }
        public int totalGold { get; set; }

        public PlayerStatsEntity()
        {
            tableName = TableName;
        }

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}
