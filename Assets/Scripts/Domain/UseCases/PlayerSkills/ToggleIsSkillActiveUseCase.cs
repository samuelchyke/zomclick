using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerSkills {
    public interface IToggleSkillActiveUseCase
    {
        Task Invoke(string playerSkillId);
    }

    public class ToggleSkillActiveUseCaseImpl : IToggleSkillActiveUseCase, IInitializable
    {
        private readonly IPlayerSkillsRepository _playerSkillsRepository;

        [Inject]
        public ToggleSkillActiveUseCaseImpl(IPlayerSkillsRepository playerSkillsRepository)
        {
            _playerSkillsRepository = playerSkillsRepository;
        }

        public void Initialize()
        {
            Debug.Log("ToggleIsSkillActiveUseCase Initialized");
        }

        public Task Invoke(string playerSkillId)
        {
            return _playerSkillsRepository.ToggleIsSkillActive(playerSkillId);
        }
    }
}
