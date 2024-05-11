using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IOnEnemyDeathUseCase {
    public Task Invoke();
}

public class OnEnemyDeathUseCaseImpl : IOnEnemyDeathUseCase, IInitializable
{
    IEnemyRepository enemyRepository;

    [Inject]
    public OnEnemyDeathUseCaseImpl(IEnemyRepository enemyRepository)
    {
        this.enemyRepository = enemyRepository;
    }

    public void Initialize()
    {
        Debug.Log("OnEnemyDeathUseCaseImpl Initialized");
    }

    public async Task Invoke()
    {
        await enemyRepository.OnEnemyDeath();
    }
}