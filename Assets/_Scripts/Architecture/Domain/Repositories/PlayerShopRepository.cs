using System.Threading.Tasks;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;

public interface IPlayerShopRepository
{
    Task<PlayerShopDetails> ReadShopDetails();

    Task BuyDamage();
    Task BuyCritDamage();
    Task BuyCritRate();
    Task BuyHealth();
    
}

public class PlayerShopRepositoryImpl : IPlayerShopRepository, IInitializable
{
    IPlayerShopDao playerShopDao;
    IPlayerStatsDao playerStatsDao;

    [Inject]
    public PlayerShopRepositoryImpl(
        IPlayerShopDao playerShopDao,
        IPlayerStatsDao playerStatsDao
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

    public async Task BuyDamage()
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var playerShop = await playerShopDao.ReadShopDetails();

        playerStats.totalGold -= playerShop.damageCost;
        playerStats.baseDamage += 10;
        playerShop.damageCost += 10;

        await playerShopDao.UpdateShopDetails(playerShop);
        await playerStatsDao.UpdatePlayerStats(playerStats);
    }

    public async Task BuyCritRate()
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var playerShop = await playerShopDao.ReadShopDetails();

        playerStats.totalGold -= playerShop.critRateCost;
        playerStats.critRate += 10;
        playerShop.critRateCost += 10;
        
        await playerShopDao.UpdateShopDetails(playerShop);
        await playerStatsDao.UpdatePlayerStats(playerStats);
        // Debug.Log("Shop Repository - BuyCritRate");
    }
    
    public async Task BuyCritDamage()
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var playerShop = await playerShopDao.ReadShopDetails();

        playerStats.totalGold -= playerShop.critDamageCost;
        playerStats.critMultiplier += 10;
        playerShop.critDamageCost += 10;

        await playerShopDao.UpdateShopDetails(playerShop);
        await playerStatsDao.UpdatePlayerStats(playerStats);
        // Debug.Log("Shop Repository - BuyCritDamage");
    }
    
    public async Task BuyHealth()
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var playerShop = await playerShopDao.ReadShopDetails();

        playerStats.totalGold -= playerShop.wallHealthCost;
        playerStats.wallHealth += 10;
        playerShop.wallHealthCost += 10;

        await playerShopDao.UpdateShopDetails(playerShop);
        await playerStatsDao.UpdatePlayerStats(playerStats);
    }
}
