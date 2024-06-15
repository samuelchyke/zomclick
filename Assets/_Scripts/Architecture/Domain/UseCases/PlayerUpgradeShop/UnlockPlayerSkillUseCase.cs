using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUnlockPlayerSkillUseCase {
    Task Invoke(string playerSkillId);
}

public class UnlockPlayerSkillUseCaseImpl : IUnlockPlayerSkillUseCase, IInitializable {
    IPlayerShopRepository shopRepository;

    [Inject]
    public UnlockPlayerSkillUseCaseImpl(IPlayerShopRepository shopRepository) {
        this.shopRepository = shopRepository;
    }

    public void Initialize() {
        Debug.Log("UnlockPlayerSkillUseCase Initialized");
    }

    public Task Invoke(string playerSkillId) {
        return shopRepository.UnlockPlayerSkill(playerSkillId);
    }
}