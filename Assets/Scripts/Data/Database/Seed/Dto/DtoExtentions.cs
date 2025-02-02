using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed.Dto {
    public static class DtoExtensions {
        public static AllySkillEntity ToEntity(this AllySkillDto dto) {
            return new AllySkillEntity {
                id = dto.id,
                allyId = dto.allyId,
                isUnlocked = dto.isUnlocked,
                description = dto.description,
                unlockLevel = dto.unlockLevel,
                buff = dto.buff
            };
        }

        public static AllyStatsEntity ToEntity(this AllyStatsDto dto) {
            return new AllyStatsEntity {
                id = dto.id,
                name = dto.name,
                level = dto.level,
                attackSpeed = dto.attackSpeed,
                baseDamage = dto.baseDamage,
                critRate = dto.critRate,
                critMultiplier = dto.critMultiplier,
                totalDamage = dto.totalDamage,
                unlockCost = dto.unlockCost,
                upgradeCost = dto.upgradeCost,
                isUnlocked = dto.isUnlocked,
                lore = dto.lore
            };
        }

        public static BossStatsEntity ToEntity(this BossStatsDto dto) {
            return new BossStatsEntity {
                id = dto.id,
                totalHealth = dto.totalHealth,
                damage = dto.damage,
                attackSpeed = dto.attackSpeed,
                movementSpeed = dto.movementSpeed,
                goldDropAmount = dto.goldDropAmount
            };
        }

        public static EnemyStatsEntity ToEntity(this EnemyStatsDto dto) {
            return new EnemyStatsEntity {
                id = dto.id,
                totalHealth = dto.totalHealth,
                damage = dto.damage,
                attackSpeed = dto.attackSpeed,
                movementSpeed = dto.movementSpeed,
                goldDropAmount = dto.goldDropAmount
            };
        }

        public static EnemyWaveEntity ToEntity(this EnemyWaveDto dto) {
            return new EnemyWaveEntity {
                id = dto.id,
                round = dto.round,
                spawnLimit = dto.spawnLimit,
                spawnTotal = dto.spawnTotal,
                enemiesKilled = dto.enemiesKilled
            };
        }

        public static MetadataEntity ToEntity(this MetadataDto dto) {
            return new MetadataEntity {
                id = "data_version",
                dataVersion = dto.dataVersion
            };
        }

        public static PlayerShopEntity ToEntity(this PlayerShopDto dto) {
            return new PlayerShopEntity {
                id = dto.id,
                wallHealthCost = dto.wallHealthCost,
                damageCost = dto.damageCost,
                critDamageCost = dto.critDamageCost,
                critRateCost = dto.critRateCost
            };
        }

        public static PlayerSkillEntity ToEntity(this PlayerSkillDto dto) {
            return new PlayerSkillEntity {
                id = dto.id,
                isUnlocked = dto.isUnlocked,
                unlockLevel = dto.unlockLevel,
                level = dto.level,
                duration = dto.duration,
                coolDown = dto.coolDown,
                buff = dto.buff,
                unlockCost = dto.unlockCost,
                upgradeCost = dto.upgradeCost,
                isActive = dto.isActive
            };
        }

        public static PlayerStatsEntity ToEntity(this PlayerStatsDto dto) {
            return new PlayerStatsEntity {
                id = dto.id,
                level = dto.level,
                baseDamage = dto.baseDamage,
                critRate = dto.critRate,
                critMultiplier = dto.critMultiplier,
                totalDamage = dto.totalDamage,
                totalGold = dto.totalGold
            };
        }
    }
}