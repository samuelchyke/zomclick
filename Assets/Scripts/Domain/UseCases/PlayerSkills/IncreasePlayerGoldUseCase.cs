using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerSkills {
    public interface IIncreasePlayerGoldUseCase
    {
        Task Invoke();
    }

    public class IncreasePlayerGoldUseCaseImpl : IIncreasePlayerGoldUseCase, IInitializable
    {
        private readonly IPlayerSkillsRepository _playerSkillsRepository;

        [Inject]
        public IncreasePlayerGoldUseCaseImpl(IPlayerSkillsRepository playerSkillsRepository)
        {
            _playerSkillsRepository = playerSkillsRepository;
        }

        public void Initialize()
        {
            Debug.Log("IncreasePlayerGoldUseCase Initialized");
        }

        public Task Invoke()
        {
            return _playerSkillsRepository.IncreasePlayerGold();
        }
    }
}