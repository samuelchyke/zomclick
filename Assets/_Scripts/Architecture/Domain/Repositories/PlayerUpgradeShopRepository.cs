using System.Threading.Tasks;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;

public interface IPlayerUpgradeShopRepository
{
    Task<PlayerUpgradeShopDetails> ReadShopDetails();

    Observable<PlayerUpgradeShopDetails> ObserveShopDetails();

    Task UpdateShopDetails(PlayerUpgradeShopDetails shopDetails);
}

public class PlayerUpgradeShopRepositoryImpl : IPlayerUpgradeShopRepository, IInitializable
{
    IPlayerUpgradeShopDao shopDao;

    [Inject]
    public PlayerUpgradeShopRepositoryImpl(IPlayerUpgradeShopDao shopDao)
    {
        this.shopDao = shopDao;
    }

    public void Initialize()
    {
        Debug.Log("Shop Repository Initialized");
    }

    public async Task<PlayerUpgradeShopDetails> ReadShopDetails()
    {
        var entity = await shopDao.ReadShopDetails();
        return new PlayerUpgradeShopDetailsBuilder().ToDomain(entity);
    }

    public Observable<PlayerUpgradeShopDetails> ObserveShopDetails()
    {
        return shopDao.ObserveShopDetails()
        .Select(entity =>
        {
            Debug.Log("Shop Repository - ReadShopDetails: " + entity.id);
            return new PlayerUpgradeShopDetailsBuilder().ToDomain(entity);
        });
    }

    public async Task UpdateShopDetails(PlayerUpgradeShopDetails shopDetails)
    {
        PlayerUpgradeShopEntity entity = new PlayerUpgradeShopDetailsBuilder().ToEntity(shopDetails);
        await shopDao.UpdateAndFetchShopDetails(entity);
    }
}
