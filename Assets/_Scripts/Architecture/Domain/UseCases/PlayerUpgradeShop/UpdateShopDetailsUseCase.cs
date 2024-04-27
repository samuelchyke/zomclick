using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpdateShopDetailsUseCase {
    public Task Invoke(PlayerUpgradeShopDetails shopDetails);
}

public class UpdateShopDetailsUseCaseImpl : IUpdateShopDetailsUseCase, IInitializable
{
    IPlayerUpgradeShopRepository shopRepository;

    [Inject]
    public UpdateShopDetailsUseCaseImpl(IPlayerUpgradeShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("UpdateAndFetchShopDetailsUseCaseImpl Initialized");
    }

    public async Task Invoke(PlayerUpgradeShopDetails shopDetails)
    {
        await shopRepository.UpdateShopDetails(shopDetails);
    }
}
