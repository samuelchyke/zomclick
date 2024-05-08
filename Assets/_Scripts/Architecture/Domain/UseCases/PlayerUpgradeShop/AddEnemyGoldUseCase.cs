using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IAddEnemyGoldUseCase {
    public Task Invoke();
}

public class AddEnemyGoldUseCaseImpl : IAddEnemyGoldUseCase, IInitializable
{
    IPlayerShopRepository shopRepository;

    [Inject]
    public AddEnemyGoldUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("AddEnemyGoldUseCase Initialized");
    }

    public Task Invoke()
    {
        return shopRepository.AddEnemyGold();
    }
}

