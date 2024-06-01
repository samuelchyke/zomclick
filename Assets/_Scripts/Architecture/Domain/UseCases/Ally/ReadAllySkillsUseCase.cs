using System.Collections.Generic;
using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadAllySkillsUseCase
{
    Task<List<AllySkill>> Invoke(string allyId);
}

public class ReadAllySkillsUseCaseImpl : IReadAllySkillsUseCase, IInitializable
{
    IAllyRepository allyRepository;

    [Inject]
    public ReadAllySkillsUseCaseImpl(IAllyRepository allyRepository)
    {
        this.allyRepository = allyRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadAllySkillsUseCaseImpl Initialized");
    }

    public Task<List<AllySkill>> Invoke(string allyId)
    {
        return allyRepository.ReadAllySkills(allyId);
    }
}