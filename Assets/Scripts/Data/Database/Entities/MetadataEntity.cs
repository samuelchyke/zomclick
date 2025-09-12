using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(MetadataEntity.TableName)]
    public record MetadataEntity : SeedEntity
    {
        public const string TableName = "metadata";
        
        [PrimaryKey]
        public string id { get; set; }
        public int dataVersion { get; set; }

        public MetadataEntity()
        {
            tableName = TableName;
        }

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}