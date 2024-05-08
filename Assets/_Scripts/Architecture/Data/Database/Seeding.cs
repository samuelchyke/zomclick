using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public class Seeding
{
    PlayerStatsEntity DefaultPlayerStats { get; set; }
    PlayerShopEntity DefaultShopDetails { get; set; }
    EnemyStatsEntity DefaultEnemyStats { get; set; }
    BossStatsEntity DefaultBossStats { get; set; }
    EnemyWaveEntity DefaultEnemyWave { get; set; }
    List<AllyStatsEntity> allyStatsEntities{ get; set; }

    private SQLiteConnection dbConnection;

    public Seeding(SQLiteConnection dbConnection)
    {
        this.dbConnection = dbConnection;
        
        DefaultPlayerStats = new PlayerStatsEntity
        {
            id = Guid.NewGuid().ToString(),
            wallHealth = 3,
            baseDamage = 1,
            critRate = 1,
            critMultiplier = 1,
            totalDamage = 1,
            totalGold = 5000
        };

        DefaultShopDetails = new PlayerShopEntity
        {
            id = Guid.NewGuid().ToString(),
            wallHealthCost = 10,
            damageCost = 10,
            critRateCost = 10,
            critDamageCost = 10
        };

        DefaultEnemyStats = new EnemyStatsEntity
        {
            id = Guid.NewGuid().ToString(),
            totalHealth = 2,
            damage = 1,
            attackSpeed = 1,
            movementSpeed = 1,
            goldDropAmount = 10
        };

        DefaultBossStats = new BossStatsEntity
        {
            id = Guid.NewGuid().ToString(),
            totalHealth = 100,
            damage = 1,
            attackSpeed = 1,
            movementSpeed = 1,
            goldDropAmount = 100
        };

        DefaultEnemyWave = new EnemyWaveEntity
        {
            id = Guid.NewGuid().ToString(),
            round = 1,
            spawnLimit = 3,
            spawnTotal = 0,
            enemiesKilled = 0
        };

        allyStatsEntities = new List<AllyStatsEntity>
        {
            new AllyStatsEntity
            {
                id = Guid.NewGuid().ToString(),
                name = "Ally One",
                attackSpeed = 15,
                baseDamage = 10,
                critRate = 5,
                critMultiplier = 1.5f,
                totalDamage = 10,
                unlockCost = 10,
                upgradeCost = 10,
                isUnlocked = false
            },
            new AllyStatsEntity
            {
                id = Guid.NewGuid().ToString(),
                name = "Ally Two",
                attackSpeed = 12,
                baseDamage = 15,
                critRate = 10,
                critMultiplier = 1.2f,
                totalDamage = 15,
                unlockCost = 10,
                upgradeCost = 10,
                isUnlocked = false
            }
        };


    }

    public void SeedDatabase(bool seedDb)
    {
        if(!seedDb) return;
        dbConnection.RunInTransaction(() =>
        {
            try 
            {
                SeedPlayerStats();
                SeedShopDetails();
                SeedEnemyStats();
                SeedEnemyWaves();
                SeedBossStats();
                SeedAllyStats();
            }
            catch (Exception ex)
            {
                dbConnection.Rollback();
                
                Debug.LogError("Error seeding database: " + ex.Message);
            }
        });
    }

    private void SeedPlayerStats()
    {
        dbConnection.InsertOrReplace(DefaultPlayerStats);
    }

    private void SeedShopDetails()
    {
        dbConnection.InsertOrReplace(DefaultShopDetails);
    }

    private void SeedEnemyStats()
    {
       dbConnection.InsertOrReplace(DefaultEnemyStats);
    }

    private void SeedEnemyWaves()
    {
       dbConnection.InsertOrReplace(DefaultEnemyWave);
    }
    
    private void SeedBossStats()
    {
       dbConnection.InsertOrReplace(DefaultBossStats);
    }

    private void SeedAllyStats()
    {
        foreach (var ally in allyStatsEntities)
        {
            dbConnection.InsertOrReplace(ally);
        }
    }
}