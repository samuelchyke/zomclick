using System;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dto {
    [Serializable]
    public class MetadataDto : SeedDto
    {
        public int dataVersion { get; set; }

        public SeedEntity toEntity()
        {
            return new MetadataEntity
            {
                dataVersion = dataVersion
            };
        }
    }
}