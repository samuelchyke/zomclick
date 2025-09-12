using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(ArtifactShopEntity.TableName)]
    public record ArtifactShopEntity : SeedEntity
    {
        public const string TableName = "artifactShop";

        [PrimaryKey]
        public string id { get; set; }
        public int artifactUnlockCost { get; set; }

        public ArtifactShopEntity()
        {
            tableName = TableName;
        }   

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}