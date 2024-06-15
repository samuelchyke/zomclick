using System.Threading.Tasks;
using System.Collections.Generic;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadPlayerSkillsUseCase {
    public Task<List<PlayerSkill>> Invoke();
}

public class ReadPlayerSkillsUseCaseImpl : IReadPlayerSkillsUseCase, IInitializable
{
    IPlayerShopRepository shopRepository;

    [Inject]
    public ReadPlayerSkillsUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadPlayerSkillsUseCase Initialized");
    }

    public Task<List<PlayerSkill>> Invoke()
    {
        return shopRepository.ReadPlayerSkills();
    }
}