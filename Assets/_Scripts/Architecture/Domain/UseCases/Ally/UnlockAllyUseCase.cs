using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUnlockAllyUseCase {
    Task Invoke(string allyId);
}

public class UnlockAllyUseCaseImpl : IUnlockAllyUseCase, IInitializable {
    IAllyRepository allyRepository;

    [Inject]
    public UnlockAllyUseCaseImpl(IAllyRepository allyRepository) {
        this.allyRepository = allyRepository;
    }

    public void Initialize() {
        Debug.Log("UpdateAllyStatsUseCaseImpl Initialized");
    }

    public Task Invoke(string allyId) {
        return allyRepository.UnlockAlly(allyId);
    }
}