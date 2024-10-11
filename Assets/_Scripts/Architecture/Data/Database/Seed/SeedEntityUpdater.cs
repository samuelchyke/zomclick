using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public interface ISeedEntityUpdater
{
    void UpsertDataInTransaction(
        List<(string, List<SeedEntity>)> preparedData,
        // List<DataMigration> dataMigrations,
        int? dataVersionOnDevice
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
        List<(string, List<SeedEntity>)> preparedData,
        // List<DataMigration> dataMigrations,
        int? dataVersionOnDevice
    )
    {
        database.RunInTransaction(() =>
        {
            try
            {
                // Disable foreign key checks temporarily
                // database.Execute("PRAGMA defer_foreign_keys = true");

                // Batch upsert entities
                foreach (var data in preparedData)
                {
                    BatchUpsertEntities(data);
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

    public void BatchUpsertEntities((string, List<SeedEntity>) data)
    {
        var entities = data.Item2;
        if (entities == null || entities.Count == 0) return;

        var instanceOfEntity = entities.First();

        try
        {
            // Generate the update clause for the query
            var updateString = string.Join(", ", instanceOfEntity.Columns.Select(name => $"\"{name}\" = excluded.\"{name}\""));

            // Generate the placeholder string for named parameters
            var valuesPlaceholder = string.Join(", ", instanceOfEntity.FilteredMembers.Select(m => $"@{m.Name}"));

            // Build the full insert with ON CONFLICT query
            var query = $@"
                INSERT INTO {instanceOfEntity.tableName} ({string.Join(", ", instanceOfEntity.FilteredMembers.Select(m => $"\"{m.Name}\""))})
                VALUES ({valuesPlaceholder})
                ON CONFLICT ({instanceOfEntity.KeyNames})
                DO UPDATE SET {updateString}
            ";

            Debug.Log($"Generated SQL Query: {query}");  // Log the generated SQL

            // Create a new command
            var command = database.CreateCommand(query);

            // Iterate through entities and bind parameters
            foreach (var entity in entities)
            {
                Debug.Log($"Seeding entity: {entity}"); // Log the entity
                
                // Create a new command for each entity

                // Log the values before binding
                foreach (var property in instanceOfEntity.FilteredMembers)
                {
                    var value = property.GetValue(entity);
                    Debug.Log($"Binding {property.Name} with value: {value}");
                }

                // Bind current entity data to the prepared statement
                entity.BindStatement(command, instanceOfEntity.FilteredMembers);

                // Log if the command is executed successfully or fails
                try
                {
                    command.ExecuteNonQuery();  // Execute the query for the current entity
                    Debug.Log($"Entity successfully inserted/updated in {instanceOfEntity.tableName}.");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error inserting entity into {instanceOfEntity.tableName}: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"== Statement exception ==: {ex.Message}");
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
