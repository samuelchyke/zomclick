using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Ally {
    public interface IUpgradeAllyStatsUseCase {
        Task Invoke(string allyId);
    }

    public class UpgradeAllyStatsUseCaseImpl : IUpgradeAllyStatsUseCase, IInitializable {
        IAllyRepository allyRepository;

        [Inject]
        public UpgradeAllyStatsUseCaseImpl(IAllyRepository allyRepository) {
            this.allyRepository = allyRepository;
        }

        public void Initialize() {
            Debug.Log("UpgradeAllyUseCaseImpl Initialized");
        }

        public Task Invoke(string allyId) {
            return allyRepository.UpgradeAllyStats(allyId);
        }
    }
}