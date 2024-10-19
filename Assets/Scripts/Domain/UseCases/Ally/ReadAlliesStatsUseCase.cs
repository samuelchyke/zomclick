using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Ally {
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
}