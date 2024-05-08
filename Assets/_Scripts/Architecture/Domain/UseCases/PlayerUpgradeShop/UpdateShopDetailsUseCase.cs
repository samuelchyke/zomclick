using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpdateShopDetailsUseCase {
    public Task Invoke(PlayerShopDetails shopDetails);
}

public class UpdateShopDetailsUseCaseImpl : IUpdateShopDetailsUseCase, IInitializable
{
    IPlayerShopRepository shopRepository;

    [Inject]
    public UpdateShopDetailsUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("UpdateAndFetchShopDetailsUseCaseImpl Initialized");
    }

    public async Task Invoke(PlayerShopDetails shopDetails)
    {
        await shopRepository.UpdateShopDetails(shopDetails);
    }
}
