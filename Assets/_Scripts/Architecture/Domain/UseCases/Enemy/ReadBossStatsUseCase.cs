using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadBossStatsUseCase {
    Task<BossStats> Invoke();
}

public class ReadBossStatsUseCaseImpl : IReadBossStatsUseCase, IInitializable
{
    IEnemyRepository enemyRepository;

    [Inject]
    public ReadBossStatsUseCaseImpl(IEnemyRepository enemyRepository)
    {
        this.enemyRepository = enemyRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadBossStatsUseCase Initialized");
    }

    public Task<BossStats> Invoke()
    {
        return enemyRepository.ReadBossStats();
    }
}
