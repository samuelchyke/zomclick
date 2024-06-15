using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using Debug = UnityEngine.Debug;

public class Seeding
{
    private readonly SQLiteConnection dbConnection;
    
    public Seeding(SQLiteConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public void SeedDatabase(bool seedDb)
    {
        if (!seedDb) return;
        
        dbConnection.RunInTransaction(() =>
        {
            try
            {
                SeedPlayerStats();
                SeedPlayerSkills();
                SeedShopDetails();
                SeedEnemyStats();
                SeedEnemyWaves();
                SeedBossStats();
                SeedAllyStats();
                SeedAllySkills();
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
        var playerStats = new PlayerStatsEntity
        {
            id = Guid.NewGuid().ToString(),
            wallHealth = 3,
            baseDamage = 1,
            critRate = 1,
            critMultiplier = 1,
            totalDamage = 1,
            totalGold = 5000
        };
        
        dbConnection.InsertOrReplace(playerStats);
    }

    private void SeedPlayerSkills()
    {
        var playerSkills = new List<PlayerSkillEntity>
        {
            new PlayerSkillEntity {
                id = "bomb_id",
                isUnlocked = false,
                buff = 10,
                unlockLevel = 10,
                unlockCost = 100,
                upgradeCost = 10,
                duration = 10,
                coolDown = 30,
                level = 1
            },
            new PlayerSkillEntity {
                id = "turret_id",
                isUnlocked = false,
                buff = 10,
                unlockLevel = 10,
                unlockCost = 100,
                upgradeCost = 10,
                duration = 10,
                coolDown = 30,
                level = 1
            },
            new PlayerSkillEntity {
                id = "range_id",
                isUnlocked = false,
                buff = 10,
                unlockLevel = 10,
                unlockCost = 100,
                upgradeCost = 10,
                duration = 10,
                coolDown = 30,
                level = 1
            },
        };
        
        dbConnection.InsertOrReplace(playerSkills);
    }


    private void SeedShopDetails()
    {
        var shopDetails = new PlayerShopEntity
        {
            id = Guid.NewGuid().ToString(),
            wallHealthCost = 10,
            damageCost = 10,
            critRateCost = 10,
            critDamageCost = 10
        };
        
        dbConnection.InsertOrReplace(shopDetails);
    }

    private void SeedEnemyStats()
    {
        var enemyStats = new EnemyStatsEntity
        {
            id = Guid.NewGuid().ToString(),
            totalHealth = 2,
            damage = 1,
            attackSpeed = 1,
            movementSpeed = 1,
            goldDropAmount = 10
        };
        
        dbConnection.InsertOrReplace(enemyStats);
    }

    private void SeedEnemyWaves()
    {
        var enemyWave = new EnemyWaveEntity
        {
            id = Guid.NewGuid().ToString(),
            round = 1,
            spawnLimit = 3,
            spawnTotal = 0,
            enemiesKilled = 0
        };
        
        dbConnection.InsertOrReplace(enemyWave);
    }
    
    private void SeedBossStats()
    {
        var bossStats = new BossStatsEntity
        {
            id = Guid.NewGuid().ToString(),
            totalHealth = 100,
            damage = 1,
            attackSpeed = 1,
            movementSpeed = 1,
            goldDropAmount = 100
        };
        
        dbConnection.InsertOrReplace(bossStats);
    }

    private void SeedAllyStats()
    {
        var allyStats = new List<AllyStatsEntity>
        {
            new AllyStatsEntity
            {
                id = "john_id",
                name = "John",
                attackSpeed = 15,
                level = 1,
                baseDamage = 10,
                critRate = 5,
                critMultiplier = 1.5f,
                totalDamage = 10,
                unlockCost = 100,
                upgradeCost = 10,
                isUnlocked = false,
                lore = "John the gunslinger came from texas"
            },
            new AllyStatsEntity
            {
                id = "doe_id",
                name = "Doe",
                level = 1,
                attackSpeed = 12,
                baseDamage = 15,
                critRate = 10,
                critMultiplier = 1.2f,
                totalDamage = 15,
                unlockCost = 200,
                upgradeCost = 10,
                isUnlocked = false,
                lore = "Johns partner in crime"
            },
            new AllyStatsEntity
            {
                id = "rod_id",
                name = "Rod",
                attackSpeed = 15,
                level = 1,
                baseDamage = 10,
                critRate = 5,
                critMultiplier = 1.5f,
                totalDamage = 10,
                unlockCost = 100,
                upgradeCost = 10,
                isUnlocked = false,
                lore = "John the gunslinger came from texas"
            },
            new AllyStatsEntity
            {
                id = "don_id",
                name = "Don",
                level = 1,
                attackSpeed = 12,
                baseDamage = 15,
                critRate = 10,
                critMultiplier = 1.2f,
                totalDamage = 15,
                unlockCost = 200,
                upgradeCost = 10,
                isUnlocked = false,
                lore = "Johns partner in crime"
            },
            new AllyStatsEntity
            {
                id = "joe_id",
                name = "Joe",
                attackSpeed = 15,
                level = 1,
                baseDamage = 10,
                critRate = 5,
                critMultiplier = 1.5f,
                totalDamage = 10,
                unlockCost = 100,
                upgradeCost = 10,
                isUnlocked = false,
                lore = "John the gunslinger came from texas"
            },
            new AllyStatsEntity
            {
                id = "doen_id",
                name = "Doen",
                level = 1,
                attackSpeed = 12,
                baseDamage = 15,
                critRate = 10,
                critMultiplier = 1.2f,
                totalDamage = 15,
                unlockCost = 200,
                upgradeCost = 10,
                isUnlocked = false,
                lore = "Johns partner in crime"
            }
        };
        
        foreach (var ally in allyStats)
        {
            dbConnection.InsertOrReplace(ally);
        }
    }

    private void SeedAllySkills()
    {
        var allySkills = new List<AllySkillEntity>
        {
            new AllySkillEntity
            {
                id = Guid.NewGuid().ToString(),
                allyId = "john_id",
                isUnlocked = false,
                description = "Unlock John Unlimited Power",
                unlockLevel = 10,
                buff = 10
            },
            new AllySkillEntity
            {
                id = Guid.NewGuid().ToString(),
                allyId = "john_id",
                isUnlocked = false,
                description = "Unlock John Unlimited Power 2",
                unlockLevel = 25,
                buff = 10
            },
            new AllySkillEntity
            {
                id = Guid.NewGuid().ToString(),
                allyId = "doe_id",
                isUnlocked = false,
                description = "Unlock Doe Unlimited Power",
                unlockLevel = 10,
                buff = 10
            },
            new AllySkillEntity
            {
                id = Guid.NewGuid().ToString(),
                allyId = "doe_id",
                isUnlocked = false,
                description = "Unlock Doe Unlimited Power 2",
                unlockLevel = 25,
                buff = 10
            }
        };
        
        foreach (var allySkill in allySkills)
        {
            dbConnection.InsertOrReplace(allySkill);
        }
    }
}
