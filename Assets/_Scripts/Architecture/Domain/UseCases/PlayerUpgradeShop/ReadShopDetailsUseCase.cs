using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadShopDetailsUseCase {
    public Task<PlayerShopDetails> Invoke();
}

public class ReadShopDetailsUseCaseImpl : IReadShopDetailsUseCase, IInitializable
{
    IPlayerShopRepository shopRepository;

    [Inject]
    public ReadShopDetailsUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadEnemyStatsUseCase Initialized");
    }

    public Task<PlayerShopDetails> Invoke()
    {
        return shopRepository.ReadShopDetails();
    }
}
