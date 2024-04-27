using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpdateBossStatsUseCase {
    Task Invoke(BossStats bossStats);
}

public class UpdateBossStatsUseCaseImpl : IUpdateBossStatsUseCase, IInitializable
{
    IEnemyRepository enemyRepository;

    [Inject]
    public UpdateBossStatsUseCaseImpl(IEnemyRepository enemyRepository)
    {
        this.enemyRepository = enemyRepository;
    }

    public void Initialize()
    {
        Debug.Log("UpdateBossStatsUseCaseImpl Initialized");
    }

    public async Task Invoke(BossStats bossStats)
    {
        await enemyRepository.UpdateBossStats(bossStats);
    }
}