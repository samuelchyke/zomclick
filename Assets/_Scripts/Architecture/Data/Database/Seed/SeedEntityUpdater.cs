using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public interface ISeedEntityUpdater
{
    void UpsertDataInTransaction(
        List<(string, List<SeedEntity>)> data,
        // List<DataMigration> dataMigrations,
        int dataVersionOnDevice
    );
}


public class SeedEntityUpdaterImpl : ISeedEntityUpdater
{
    private readonly SQLiteConnection database;

    public SeedEntityUpdaterImpl(SQLiteConnection database)
    {
        this.database = database;
    }

    public void UpsertDataInTransaction(
        List<(string, List<SeedEntity>)> data,
        // List<DataMigration> dataMigrations,
        int dataVersionOnDevice)
    {
        database.RunInTransaction(() =>
        {
            try
            {
                // Disable foreign key checks temporarily
                // database.Execute("PRAGMA defer_foreign_keys = true");

                // Batch upsert entities
                foreach (var dataSet in data)
                {
                    BatchUpsertEntities(dataSet);
                }

                // Apply data migrations if applicable
                // if (dataVersionOnDevice > 0)
                // {
                    // ApplyDataMigrations(dataMigrations, dataVersionOnDevice);
                // }

                // Delete entities that are missing from the dataset
                // foreach (var dataSet in data)
                // {
                    // DeleteEntitiesIfMissing(dataSet);
                // }

                // Re-enable foreign key checks
                // database.Execute("PRAGMA defer_foreign_keys = false");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error during database seeding: {ex.Message}");
                throw;
            }
        });
    }

    private void BatchUpsertEntities((string TableName, List<SeedEntity> Entities) dataSet)
    {
        var entities = dataSet.Entities;
        if (entities == null || entities.Count == 0) return;

        var firstEntity = entities[0];

        try
        {
            // Construct the upsert query dynamically
            var columnNames = firstEntity.FilteredMembers.Select(p => p.Name).ToArray();
            var valuesPlaceholder = string.Join(", ", columnNames.Select(_ => "?"));
            var updateString = string.Join(", ", columnNames.Select(name => $"{name} = excluded.{name}"));

            string query = $@"
                INSERT INTO {firstEntity.tableName} ({string.Join(", ", columnNames)})
                VALUES ({valuesPlaceholder})
                ON CONFLICT ({firstEntity.KeyNames})
                DO UPDATE SET {updateString}";

            var command = database.CreateCommand(query);

            foreach (var entity in entities)
            {
                // Bind the parameters explicitly for each entity
                entity.BindStatement(command, firstEntity.FilteredMembers);

                // Execute the command
                command.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"BatchUpsertEntities error: {ex.Message}");
        }
    }

    // private void DeleteEntitiesIfMissing((string TableName, List<SeedEntity> Entities) dataSet)
    // {
    //     try
    //     {
    //         var entities = dataSet.Entities;
    //         string query;

    //         if (entities == null || entities.Count == 0)
    //         {
    //             // Delete all rows if no entities are provided
    //             query = $"DELETE FROM {dataSet.TableName}";
    //         }
    //         else
    //         {
    //             var ids = string.Join(", ", entities.Select(e => $"'{e.Identifier}'"));
    //             var firstEntity = entities[0];

    //             // Delete entities that are not present in the new data set
    //             query = $@"
    //                 DELETE FROM {firstEntity.tableName}
    //                 WHERE {firstEntity.KeyNamesSQLSeparator} NOT IN ({ids})";
    //         }

    //         var command = database.CreateCommand(query);
    //         command.ExecuteNonQuery();
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError($"DeleteEntitiesIfMissing error: {ex.Message}");
    //     }
    // }

    // private void ApplyDataMigrations(List<DataMigration> dataMigrations, int dataVersionOnDevice)
    // {
    //     try
    //     {
    //         foreach (var migration in dataMigrations)
    //         {
    //             if (migration.DataVersion > dataVersionOnDevice)
    //             {
    //                 migration.Migrate(database);
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError($"DataMigration error: {ex.Message}");
    //     }
    // }
}

    // private void ApplyDataMigrations(List<DataMigration> dataMigrations, int dataVersionOnDevice)
    // {
    //     try
    //     {
    //         foreach (var migration in dataMigrations)
    //         {
    //             if (migration.DataVersion > dataVersionOnDevice)
    //             {
    //                 migration.Migrate(database);
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Debug.LogError($"DataMigration error: {ex.Message}");
    //     }
    // }
