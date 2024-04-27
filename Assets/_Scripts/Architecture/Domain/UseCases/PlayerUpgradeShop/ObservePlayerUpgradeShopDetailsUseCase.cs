using System.Threading.Tasks;
using R3;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IObservePlayerUpgradeShopDetailsUseCase {
    public Observable<PlayerUpgradeShopDetails> Invoke();
}

public class ObservePlayerUpgradeShopDetailsUseCaseImpl : IObservePlayerUpgradeShopDetailsUseCase, IInitializable
{
    IPlayerUpgradeShopRepository shopRepository;

    [Inject]
    public ObservePlayerUpgradeShopDetailsUseCaseImpl(IPlayerUpgradeShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("ObserveEnemyStatsUseCase Initialized");
    }

    public Observable<PlayerUpgradeShopDetails> Invoke()
    {
        return shopRepository.ObserveShopDetails();
    }
}

