using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IBuyDamageUseCase
{
    Task Invoke();
}

public class BuyDamageUseCaseImpl : IBuyDamageUseCase, IInitializable
{
    private readonly IPlayerShopRepository _shopRepository;

    [Inject]
    public BuyDamageUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("BuyDamageUseCase Initialized");
    }

    public Task Invoke()
    {
        return _shopRepository.BuyDamage();
    }
}
