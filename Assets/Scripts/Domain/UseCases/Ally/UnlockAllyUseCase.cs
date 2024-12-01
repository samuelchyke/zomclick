using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Ally {
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
}