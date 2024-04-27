using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpdateEnemyWaveDetailsUseCase {
    Task Invoke(EnemyWaveDetails waveDetails);
}

public class UpdateEnemyWaveDetailsUseCaseImpl : IUpdateEnemyWaveDetailsUseCase, IInitializable
{
    IEnemyRepository enemyRepository;

    [Inject]
    public UpdateEnemyWaveDetailsUseCaseImpl(IEnemyRepository enemyRepository)
    {
        this.enemyRepository = enemyRepository;
    }

    public void Initialize()
    {
        Debug.Log("UpdateEnemyWaveDetailsUseCase Initialized");
    }

    public async Task Invoke(EnemyWaveDetails waveDetails)
    {
        await enemyRepository.UpdateEnemyWaveDetails(waveDetails);
    }
}