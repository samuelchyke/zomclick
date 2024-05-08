using System.Threading.Tasks;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IObservePlayerUpgradeShopDetailsUseCase {
    public Observable<PlayerShopDetails> Invoke();
}

public class ObservePlayerUpgradeShopDetailsUseCaseImpl : IObservePlayerUpgradeShopDetailsUseCase, IInitializable
{
    IPlayerShopRepository shopRepository;

    [Inject]
    public ObservePlayerUpgradeShopDetailsUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("ObserveEnemyStatsUseCase Initialized");
    }

    public Observable<PlayerShopDetails> Invoke()
    {
        return shopRepository.ObserveShopDetails();
    }
}

