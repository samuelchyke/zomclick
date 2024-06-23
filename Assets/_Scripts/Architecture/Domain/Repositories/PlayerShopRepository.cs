using System.Threading.Tasks;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.Collections.Generic;

public interface IPlayerShopRepository
{
    Task<PlayerShopDetails> ReadShopDetails();

    Task UpgradePlayerStats();

    Task<List<PlayerSkill>> ReadPlayerSkills();
    Task<PlayerSkill> ReadPlayerSkill(string playerSkillId);

    Task UnlockPlayerSkill (string playerSkillId);
    Task UpgradePlayerSkill (string playerSkillId);
}

public class PlayerShopRepositoryImpl : IPlayerShopRepository, IInitializable
{
    IPlayerShopDao playerShopDao;
    IPlayerDao playerStatsDao;

    [Inject]
    public PlayerShopRepositoryImpl(
        IPlayerShopDao playerShopDao,
        IPlayerDao playerStatsDao
        )
    {
        this.playerShopDao = playerShopDao;
        this.playerStatsDao = playerStatsDao;
    }

    public void Initialize()
    {
        Debug.Log("Shop Repository Initialized");
    }

    public async Task<PlayerShopDetails> ReadShopDetails()
    {
        Debug.Log("Shop Repository - ReadShopDetails");
        var playerShop = await playerShopDao.ReadShopDetails();
        var playerStats = await playerStatsDao.ReadPlayerStats();
        Debug.Log("Shop Repository - ReadShopDetails - PLAYER GOLD: " + playerStats.totalGold);
        return new PlayerShopDetailsBuilder().ToDomain(
            shopEntity: playerShop,
            totalGold: playerStats.totalGold
        );
    }

    public async Task UpgradePlayerStats()
    {
        Debug.Log("Shop Repository - UpgradePlayerStats");
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var playerShop = await playerShopDao.ReadShopDetails();
        playerStats.totalGold -= playerShop.damageCost;
        Debug.Log(playerStats.totalGold);
        playerStats.baseDamage += 10;
        playerShop.damageCost += 10;
        Debug.Log("Shop Repository - UpgradePlayerStats");
        Debug.Log(playerStats.totalGold);
        await playerShopDao.UpdateShopDetails(playerShop);
        await playerStatsDao.UpdatePlayerStats(playerStats);
    }

    public async Task<PlayerSkill> ReadPlayerSkill(string playerSkillId)
    {
        var entity = await playerStatsDao.ReadPlayerSkill(playerSkillId);
        return new PlayerSkillBuilder().ToDomain(entity);
    }

    public async Task<List<PlayerSkill>> ReadPlayerSkills()
    {
        var entities = await playerStatsDao.ReadPlayerSkills();
        var playerSkills = entities.Select(item => new PlayerSkillBuilder().ToDomain(item)).ToList();
        return playerSkills;
    }

    public async Task UnlockPlayerSkill(string playerSkillId)
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var playerSkill = await playerStatsDao.ReadPlayerSkill(playerSkillId);

        if (playerStats.totalGold >= playerSkill.unlockCost)
        {
            playerStats.totalGold -= playerSkill.unlockCost;
            playerSkill.isUnlocked = true;

            await playerStatsDao.UpdatePlayerStats(playerStats);
            await playerStatsDao.UpdatePlayerSkill(playerSkill);
        }
    }

    public async Task UpgradePlayerSkill(string playerSkillId)
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var playerSkill = await playerStatsDao.ReadPlayerSkill(playerSkillId);

        if (playerStats.totalGold >= playerSkill.upgradeCost)
        {
            playerStats.totalGold -= playerSkill.upgradeCost;
            playerSkill.upgradeCost += 10;
            playerSkill.level += 1;
            
            await playerStatsDao.UpdatePlayerStats(playerStats);
            await playerStatsDao.UpdatePlayerSkill(playerSkill);
        }
    }
}
