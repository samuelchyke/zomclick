using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SQLite4Unity3d;

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
            UnityEngine.Debug.LogError($"Error executing raw query: {ex.Message}");
            return null;
        }
    }

    // Checks if the incoming data version is newer than the current data version on the device
    public bool IsDataVersionNewer(int? incomingDataVersion)
    {
        var currentVersion = DataVersionOnDevice();
        return (incomingDataVersion ?? -1) > (currentVersion ?? -1);
    }

    // Retrieves the current data version from the database (assumes there's a metadata table with dataVersion)
    public int? DataVersionOnDevice()
    {
        try
        {
            var result = _db.ExecuteScalar<int>("SELECT dataVersion FROM metadata");
            return result;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"Error retrieving data version: {ex.Message}");
            return null;
        }
    }

    // Builds a list of entities from the parsed data dump and returns it as a list of table names and entities
    public List<(string, List<SeedEntity>)> BuildEntitiesList(ParsedDataDump parsedDump)
    {
        var tables = new List<(string, List<SeedEntity>)>();

        // Using GetCustomAttributes to retrieve custom attributes
        foreach (var prop in typeof(ParsedDump).GetProperties())
        {
            // Retrieve all custom attributes of type SerialNameAttribute
            var attribute = (SerialNameAttribute)prop.GetCustomAttributes(typeof(SerialNameAttribute), false)
                .FirstOrDefault();

            if (attribute != null)
            {
                var tableName = attribute.Value;
                var entities = (List<SeedEntity>)prop.GetValue(parsedDump.data);
                tables.Add((tableName, entities));
            }
        }

        tables.Add((MetadataEntity.TableName, new List<SeedEntity> { parsedDump.metadata.ToEntity() }));

        return tables;
    }
}