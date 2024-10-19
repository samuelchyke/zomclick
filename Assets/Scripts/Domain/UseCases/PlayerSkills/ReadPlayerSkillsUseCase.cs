using System.Threading.Tasks;
using System.Collections.Generic;
using Zenject;
using Debug = UnityEngine.Debug;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerSkills {
    public interface IReadPlayerSkillsUseCase {
        public Task<Models.PlayerSkills> Invoke();
    }

    public class ReadPlayerSkillsUseCaseImpl : IReadPlayerSkillsUseCase, IInitializable
    {
        IPlayerSkillsRepository playerSkillRepository;

        [Inject]
        public ReadPlayerSkillsUseCaseImpl(IPlayerSkillsRepository playerSkillRepository)
        {
            this.playerSkillRepository = playerSkillRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadPlayerSkillsUseCase Initialized");
        }

        public Task<Models.PlayerSkills> Invoke()
        {
            return playerSkillRepository.ReadPlayerSkills();
        }
    }
}