using SQLite4Unity3d;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics.CodeAnalysis;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities {
    [SuppressMessage("Style", "IDE0002:Simplify name")]
    [Table(PlayerShopEntity.TableName)]
    public class PlayerShopEntity : SeedEntity
    {
        public const string TableName = "playerShopDetails";

        [PrimaryKey]
        public string id { get; set; }
        public int wallHealthCost { get; set; }
        public int damageCost { get; set; }
        public int critDamageCost { get; set; }
        public int critRateCost { get; set; }

        public PlayerShopEntity()
        {
            tableName = TableName;
        }   

        public override List<PropertyInfo> Keys => new List<PropertyInfo>
        {
            GetType().GetProperty(nameof(id))
        };
    }
}