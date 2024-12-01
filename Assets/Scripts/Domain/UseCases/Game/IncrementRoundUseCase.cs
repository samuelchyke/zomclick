using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Game {
    public interface IIncrementRoundUseCase {
        public Task Invoke();
    }

    public class IncrementRoundUseCaseImpl : IIncrementRoundUseCase, IInitializable
    {
        IGameRepository gameRepository;

        [Inject]
        public IncrementRoundUseCaseImpl(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public void Initialize()
        {
            Debug.Log("IncrementRoundUseCaseImpl Initialized");
        }

        public async Task Invoke()
        {
            await gameRepository.IncrementRound();
        }
    }
}