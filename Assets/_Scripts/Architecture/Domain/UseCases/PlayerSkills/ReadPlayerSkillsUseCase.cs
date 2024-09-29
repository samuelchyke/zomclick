using System.Threading.Tasks;
using System.Collections.Generic;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadPlayerSkillsUseCase {
    public Task<PlayerSkills> Invoke();
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

    public Task<PlayerSkills> Invoke()
    {
        return playerSkillRepository.ReadPlayerSkills();
    }
}