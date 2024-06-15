using System.Threading.Tasks;
using System.Collections.Generic;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IReadPlayerSkillUseCase {
    public Task<PlayerSkill> Invoke(string playerSkillId);
}

public class ReadPlayerSkillUseCaseImpl : IReadPlayerSkillUseCase, IInitializable
{
    IPlayerShopRepository shopRepository;

    [Inject]
    public ReadPlayerSkillUseCaseImpl(IPlayerShopRepository shopRepository)
    {
        this.shopRepository = shopRepository;
    }

    public void Initialize()
    {
        Debug.Log("ReadPlayerSkillUseCase Initialized");
    }

    public Task<PlayerSkill> Invoke(string playerSkillId)
    {
        return shopRepository.ReadPlayerSkill(playerSkillId);
    }
}