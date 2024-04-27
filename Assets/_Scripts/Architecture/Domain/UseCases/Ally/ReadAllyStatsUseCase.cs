using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadAllyStatsUseCase {
    Task<AllyStats> Invoke(string allyId);
}

public class ReadAllyStatsUseCaseImpl : IReadAllyStatsUseCase, IInitializable {
    IAllyRepository allyRepository;

    [Inject]
    public ReadAllyStatsUseCaseImpl(IAllyRepository allyRepository) {
        this.allyRepository = allyRepository;
    }

    public void Initialize() {
        Debug.Log("ReadAllyStatsUseCaseImpl Initialized");
    }

    public Task<AllyStats> Invoke(string allyId) {
        return allyRepository.ReadAllyStats(allyId);
    }
}