using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(AllySkillEntity.TableName)]
    public record AllySkillEntity : SeedEntity
    {
        public const string TableName = "allySkills";

        [PrimaryKey]
        public string id { get; set; }
        public string allyId { get; set; }
        public bool isUnlocked { get; set; }
        public string description { get; set; }
        public int unlockLevel { get; set; }
        public int buff { get; set; }

        public AllySkillEntity()
        {
            tableName = TableName;
        }

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}