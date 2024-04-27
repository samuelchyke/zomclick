using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadAlliesStatsUseCase {
    Task<List<AllyStats>> Invoke();
}

public class ReadAlliesStatsUseCaseImpl : IReadAlliesStatsUseCase, IInitializable {
    IAllyRepository allyRepository;

    [Inject]
    public ReadAlliesStatsUseCaseImpl(IAllyRepository allyRepository) {
        this.allyRepository = allyRepository;
    }

    public void Initialize() {
        Debug.Log("ReadAlliesStatsUseCaseImpl Initialized");
    }

    public Task<List<AllyStats>> Invoke() {
        return allyRepository.ReadAlliesStats();
    }
}