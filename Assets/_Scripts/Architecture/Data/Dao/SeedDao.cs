using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLite4Unity3d;
using Debug = UnityEngine.Debug;

public interface ISeedDao
{
    int? ExecRawQuery(string query);

    bool IsDataVersionNewer(int? incomingDataVersion);

    int? DataVersionOnDevice();

    List<(string, List<SeedEntity>)> BuildEntitiesList(ParsedDataDump parsedDump);
}

public class SeedDaoImpl : ISeedDao
{
    private readonly SQLiteConnection _db;

    public SeedDaoImpl(SQLiteConnection db)
    {
        _db = db;
    }

    // Executes a raw SQL query and returns the number of rows affected
    public int? ExecRawQuery(string query)
    {
        try
        {
            return _db.Execute(query);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error executing raw query: {ex.Message}");
            return null;
        }
    }

    // Checks if the incoming data version is newer than the current data version on the device
    public bool IsDataVersionNewer(int? incomingDataVersion)
    {
        var currentVersion = DataVersionOnDevice();
        return (incomingDataVersion ?? -1) > (currentVersion ?? -1);
    }

    // Retrieves the current data version from the database
    public int? DataVersionOnDevice()
    {
        try
        {
            var result = _db.ExecuteScalar<int>("SELECT dataVersion FROM metadata");
            return result;
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error retrieving data version: {ex.Message}");
            return null;
        }
    }

    // Builds a list of entities from the parsed data dump and returns it as a list of table names and entities
    public List<(string, List<SeedEntity>)> BuildEntitiesList(ParsedDataDump parsedDump)
    {
        var tables = new List<(string, List<SeedEntity>)>();

        var fields = typeof(ParsedDump).GetFields();
        if (!fields.Any())
        {
            Debug.LogError("ParsedDump has no fields.");
            // return tables;
        }

        foreach (var field in fields)
        {
            var value = field.GetValue(parsedDump.data);

            switch (value)
            {
                // Handle collections
                case IEnumerable<SeedDto> seedDtos when seedDtos.Any():
                    tables.Add((seedDtos.First().toEntity().tableName, seedDtos.toEntities()));
                    break;

                // Handle single objects
                case SeedDto singleSeedDto:
                    tables.Add((singleSeedDto.toEntity().tableName, new List<SeedEntity> { singleSeedDto.toEntity() }));
                    break;

                default:
                    Debug.LogError($"Field {field.Name} could not be processed.");
                    break;
            }
        }

        // Add metadata if present
        if (parsedDump.metadata != null)
        {
            tables.Add((MetadataEntity.TableName, new List<SeedEntity> { parsedDump.metadata.ToEntity() }));
        }

        return tables;
    }
}
