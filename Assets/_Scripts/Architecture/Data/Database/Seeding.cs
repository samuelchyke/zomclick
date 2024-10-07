using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;
using UnityEngine;
using System.IO;

public class Seeding
{
    private readonly SQLiteConnection dbConnection;
    private readonly ISeedDao seedDao;
    
    public Seeding(SQLiteConnection dbConnection)
    {
        this.dbConnection = dbConnection;
        seedDao = new SeedDaoImpl(dbConnection);  // Initialize SeedDaoImpl
    }

    public void SeedDatabase(bool seedDb)
    {
        if (!seedDb) return;

        string jsonFilePath = Path.Combine(Application.streamingAssetsPath, "dump.json");

        Debug.Log("Loading JSON from: " + jsonFilePath);

        string jsonContent = File.ReadAllText(jsonFilePath);

        if (File.Exists(jsonFilePath))
        {
            Debug.Log("JSON Content: " + jsonContent);
        }
        else
        {
            Debug.LogError("JSON file not found: " + jsonFilePath);
            return;
        }

        ParsedDataDump parsedData = JsonUtility.FromJson<ParsedDataDump>(jsonContent);

        if (parsedData == null)
        {
            Debug.LogError("ParsedData is null. JSON may be malformed or not matching the DTO structure.");
        }

        if (parsedData?.metadata == null)
        {
            Debug.LogError("Data inside Metadata is null.");
        }

        if (parsedData?.data == null)
        {
            Debug.LogError("Data inside ParsedData is null.");
        }

        if (parsedData?.data?.playerStats == null)
        {
            Debug.LogError("PlayerStats is null inside ParsedData.");
        }
        
        // Debug.LogError(parsedData.data.playerStats.id);

        dbConnection.RunInTransaction(() =>
        {
            try
            {
                // Use the SeedDaoImpl to build entities and seed them
                var entitiesToSeed = seedDao.BuildEntitiesList(parsedData);

                foreach (var (tableName, entities) in entitiesToSeed)
                {
                    Debug.Log($"Seeding table: {tableName} with {entities.Count} entities.");
                    foreach (var entity in entities)
                    {
                        dbConnection.InsertOrReplace(entity);
                    }
                }

                Debug.Log("Seeding completed successfully.");
            }
            catch (Exception ex)
            {
                dbConnection.Rollback();
                
                Debug.LogError("Error seeding database: " + ex.Message);
            }
        });
    }

    private void SeedPlayerStats(PlayerStatsDto playerStats)
    {
        dbConnection.InsertOrReplace(playerStats.ToEntity());
    }

    private void SeedPlayerSkills(List<PlayerSkillDto> playerSkills)
    {
        foreach (var skill in playerSkills)
        {
            dbConnection.InsertOrReplace(skill.ToEntity());
        }
    }

    private void SeedShopDetails(PlayerShopDetailsDto shopDetails)
    {
        dbConnection.InsertOrReplace(shopDetails.ToEntity());
    }

    private void SeedEnemyStats(EnemyStatsDto enemyStats)
    {
        dbConnection.InsertOrReplace(enemyStats.ToEntity());
    }

    private void SeedEnemyWaves(EnemyWaveDto enemyWave)
    {
        dbConnection.InsertOrReplace(enemyWave.ToEntity());
    }

    private void SeedBossStats(BossStatsDto bossStats)
    {
        dbConnection.InsertOrReplace(bossStats.ToEntity());
    }

    private void SeedAllyStats(List<AllyStatsDto> allyStats)
    {
        foreach (var ally in allyStats)
        {
            dbConnection.InsertOrReplace(ally.ToEntity());
        }
    }

    private void SeedAllySkills(List<AllySkillDto> allySkills)
    {
        foreach (var skill in allySkills)
        {
            dbConnection.InsertOrReplace(skill.ToEntity());
        }
    }
}
