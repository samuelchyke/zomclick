using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadShopDetailsUseCase {
    public Task<PlayerUpgradeShopDetails> Invoke();
}

public class ReadShopDetailsUseCaseImpl : IReadShopDetailsUseCase, IInitializable
{
    IPlayerUpgradeShopRepository shopRepository;

    [Inject]
    public ReadShopDetailsUseCaseImpl(IPlayerUpgradeShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadEnemyStatsUseCase Initialized");
    }

    public Task<PlayerUpgradeShopDetails> Invoke()
    {
        return shopRepository.ReadShopDetails();
    }
}

