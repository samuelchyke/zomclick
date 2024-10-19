using System.Collections.Generic;
using System.Linq;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed {
    public interface SeedDto
    {
        SeedEntity toEntity();
    }

    public static class SeedDtoExtensions
    {
        public static List<SeedEntity> toEntities(this IEnumerable<SeedDto> seedDtos)
        {
            return seedDtos.Select(dto => dto.toEntity()).ToList();
        }
    }
}