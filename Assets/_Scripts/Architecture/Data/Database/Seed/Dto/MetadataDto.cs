using System;

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