using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpdateEnemyStatsUseCase {
    public Task Invoke(EnemyStats enemyStats);
}

public class UpdateEnemyStatsUseCaseImpl : IUpdateEnemyStatsUseCase, IInitializable
{
    IEnemyRepository enemyRepository;

    [Inject]
    public UpdateEnemyStatsUseCaseImpl(IEnemyRepository enemyRepository)
    {
        this.enemyRepository = enemyRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadShopDetailsUseCaseImpl Initialized");
    }

    public async Task Invoke(EnemyStats enemyStats)
    {
        await enemyRepository.UpdateEnemyStats(enemyStats);
    }
}