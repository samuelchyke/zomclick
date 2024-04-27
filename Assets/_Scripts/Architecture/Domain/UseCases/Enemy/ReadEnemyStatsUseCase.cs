using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadEnemyStatsUseCase {
    public Task<EnemyStats> Invoke();
}

public class ReadEnemyStatsUseCaseImpl : IReadEnemyStatsUseCase, IInitializable
{
    IEnemyRepository enemyRepository;

    [Inject]
    public ReadEnemyStatsUseCaseImpl(IEnemyRepository enemyRepository)
    {
        this.enemyRepository = enemyRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadEnemyStatsUseCase Initialized");
    }

    public Task<EnemyStats> Invoke()
    {
        return enemyRepository.ReadEnemyStats();
    }
}
