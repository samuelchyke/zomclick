using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(ArtifactEntity.TableName)]
    public record ArtifactEntity : SeedEntity
    {
        public const string TableName = "artifacts";

        [PrimaryKey]
        public string id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public float buff { get; set; }
        public float upgradeDetails { get; set; }
        public int upgradeCost { get; set; }
        public string description { get; set; }
        public bool isUnlocked { get; set; }

        public ArtifactEntity()
        {
            tableName = TableName;
        }

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}