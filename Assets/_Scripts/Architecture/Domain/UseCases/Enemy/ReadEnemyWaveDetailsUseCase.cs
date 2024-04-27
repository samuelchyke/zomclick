using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadEnemyWaveDetailsUseCase {
    Task<EnemyWaveDetails> Invoke();
}

public class ReadEnemyWaveDetailsUseCaseImpl : IReadEnemyWaveDetailsUseCase, IInitializable
{
    IEnemyRepository enemyRepository;

    [Inject]
    public ReadEnemyWaveDetailsUseCaseImpl(IEnemyRepository enemyRepository)
    {
        this.enemyRepository = enemyRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadEnemyWaveDetailsUseCase Initialized");
    }

    public Task<EnemyWaveDetails> Invoke()
    {
        return enemyRepository.ReadEnemyWaveDetails();
    }
}
