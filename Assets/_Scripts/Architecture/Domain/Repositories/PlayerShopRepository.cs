using System.Threading.Tasks;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;

public interface IPlayerShopRepository
{
    Task<PlayerShopDetails> ReadShopDetails();

    Observable<PlayerShopDetails> ObserveShopDetails();

    Task AddEnemyGold();
    Task BuyDamage();
    Task BuyCritDamage();
    Task BuyCritRate();
    Task BuyHealth();
    
    Task UpdateShopDetails(PlayerShopDetails shopDetails);
}

public class PlayerShopRepositoryImpl : IPlayerShopRepository, IInitializable
{
    IPlayerShopDao playerShopDao;
    IEnemyDao enemyDao;
    IPlayerStatsDao playerStatsDao;

    [Inject]
    public PlayerShopRepositoryImpl(
        IPlayerShopDao playerShopDao,
        IEnemyDao enemyDao,
        IPlayerStatsDao playerStatsDao
        )
    {
        this.playerShopDao = playerShopDao;
        this.enemyDao = enemyDao;
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

    public async Task AddEnemyGold()
    {
        var playerStats = await playerStatsDao.ReadPlayerStats();
        var enemyStats = await enemyDao.ReadEnemyEntity();
        playerStats.totalGold += enemyStats.goldDropAmount;
        await playerStatsDao.UpdatePlayerStats(playerStats);
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


    public Observable<PlayerShopDetails> ObserveShopDetails()
    {
        return playerShopDao.ObserveShopDetails()
        .Select(entity =>
        {
            Debug.Log("Shop Repository - ReadShopDetails: " + entity.id);
            return new PlayerShopDetailsBuilder().ToDomain(entity, 0);
        });
    }

    public async Task UpdateShopDetails(PlayerShopDetails shopDetails)
    {
        PlayerShopEntity entity = new PlayerShopDetailsBuilder().ToEntity(shopDetails);
        await playerShopDao.UpdateShopDetails(entity);
    }
}
