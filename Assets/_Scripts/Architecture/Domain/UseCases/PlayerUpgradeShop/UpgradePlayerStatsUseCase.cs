using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpgradePlayerStatsUseCase
{
    Task Invoke();
}

public class UpgradePlayerStatsUseCaseImpl : IUpgradePlayerStatsUseCase, IInitializable
{
    private readonly IPlayerShopRepository _shopRepository;

    [Inject]
    public UpgradePlayerStatsUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("BuyDamageUseCase Initialized");
    }

    public Task Invoke()
    {
        return _shopRepository.UpgradePlayerStats();
    }
}
