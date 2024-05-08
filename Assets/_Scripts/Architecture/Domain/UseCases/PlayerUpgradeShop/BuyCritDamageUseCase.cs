using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IBuyCritDamageUseCase
{
    Task Invoke();
}

public class BuyCritDamageUseCaseImpl : IBuyCritDamageUseCase, IInitializable
{
    private readonly IPlayerShopRepository _shopRepository;

    [Inject]
    public BuyCritDamageUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        _shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("BuyCritDamageUseCase Initialized");
    }

    public Task Invoke()
    {
        return _shopRepository.BuyCritDamage();
    }
}