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
    private readonly SQLiteConnection database;
    
    public SeedDaoImpl(SQLiteConnection database)
    {
        this.database = database;
    }

    // Executes a raw SQL query and returns the number of rows affected
    public int? ExecRawQuery(string query)
    {
        try
        {
            return database.Execute(query);
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
            var result = database.ExecuteScalar<int>("SELECT dataVersion FROM metadata");
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
        var tables = typeof(ParsedDump).GetFields()
            .Select(field => field.GetValue(parsedDump.data))
            .Select(value => value 
                switch
                {
                    IEnumerable<SeedDto> seedDtos => 
                        (seedDtos.First().toEntity().tableName, seedDtos.toEntities()),

                    SeedDto seedDto => 
                        (seedDto.toEntity().tableName, new List<SeedEntity> { seedDto.toEntity() }),

                    _ => throw new InvalidOperationException("Unexpected value type.")
                }
            )
            .ToList();

        tables.Add((MetadataEntity.TableName, new List<SeedEntity> { parsedDump.metadata.ToEntity() }));
        
        return tables;
    }
}
