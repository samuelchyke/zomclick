using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IBuyHealthUseCase
{
    Task Invoke();
}

public class BuyHealthUseCaseImpl : IBuyHealthUseCase, IInitializable
{
    private readonly IPlayerShopRepository _shopRepository;

    [Inject]
    public BuyHealthUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("BuyHealthUseCase Initialized");
    }

    public Task Invoke()
    {
        return _shopRepository.BuyHealth();
    }
}