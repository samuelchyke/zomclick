using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Ally {
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
}