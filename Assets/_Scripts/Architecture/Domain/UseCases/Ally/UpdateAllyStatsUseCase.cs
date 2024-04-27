using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpdateAllyStatsUseCase {
    Task Invoke(AllyStats allyStats);
}

public class UpdateAllyStatsUseCaseImpl : IUpdateAllyStatsUseCase, IInitializable {
    IAllyRepository allyRepository;

    [Inject]
    public UpdateAllyStatsUseCaseImpl(IAllyRepository allyRepository) {
        this.allyRepository = allyRepository;
    }

    public void Initialize() {
        Debug.Log("UpdateAllyStatsUseCaseImpl Initialized");
    }

    public Task Invoke(AllyStats allyStats) {
        return allyRepository.UpdateAllyStats(allyStats);
    }
}