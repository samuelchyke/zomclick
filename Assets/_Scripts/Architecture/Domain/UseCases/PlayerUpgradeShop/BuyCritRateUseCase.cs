using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IBuyCritRateUseCase
{
    Task Invoke();
}

public class BuyCritRateUseCaseImpl : IBuyCritRateUseCase, IInitializable
{
    private readonly IPlayerShopRepository _shopRepository;

    [Inject]
    public BuyCritRateUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("BuyCritRateUseCase Initialized");
    }

    public Task Invoke()
    {
        return _shopRepository.BuyCritRate();
    }
}