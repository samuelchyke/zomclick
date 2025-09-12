using Zenject;
using Debug = UnityEngine.Debug;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using R3;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Player {
    public interface IObservePlayerStatsUseCase {
        public Observable<PlayerStats> Invoke();
    }

    public class ObservePlayerStatsUseCaseImpl : IObservePlayerStatsUseCase, IInitializable
    {
        IPlayerRepository playerRepository;

        [Inject]
        public ObservePlayerStatsUseCaseImpl(IPlayerRepository playerRepository)
        {
            this.playerRepository = playerRepository;
        }

        public void Initialize()
        {
            Debug.Log("ObservePlayerStatsUseCaseImpl Initialized");
        }

        public Observable<PlayerStats> Invoke()
        {
            return playerRepository.ObservePlayerStats();
        }
    }
}