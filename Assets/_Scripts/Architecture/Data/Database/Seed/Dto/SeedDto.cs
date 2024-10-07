using System.Collections.Generic;
using System.Linq;

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