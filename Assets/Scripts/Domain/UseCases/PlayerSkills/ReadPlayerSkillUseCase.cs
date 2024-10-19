using System.Threading.Tasks;
using System.Collections.Generic;
using Zenject;
using Debug = UnityEngine.Debug;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerSkills {
    public interface IReadPlayerSkillUseCase {
        public Task<PlayerSkill> Invoke(string playerSkillId);
    }

    public class ReadPlayerSkillUseCaseImpl : IReadPlayerSkillUseCase, IInitializable
    {
        IPlayerSkillsRepository playerSkillsRepository;

        [Inject]
        public ReadPlayerSkillUseCaseImpl(IPlayerSkillsRepository playerSkillsRepository)
        {
            this.playerSkillsRepository = playerSkillsRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadPlayerSkillUseCase Initialized");
        }

        public Task<PlayerSkill> Invoke(string playerSkillId)
        {
            return playerSkillsRepository.ReadPlayerSkill(playerSkillId);
        }
    }
}