using System.Threading.Tasks;
using Zenject;
using Debug = UnityEngine.Debug;

public interface IUpgradePlayerSkillUseCase {
    Task Invoke(string playerSkillId);
}

public class UpgradePlayerSkillUseCaseImpl : IUpgradePlayerSkillUseCase, IInitializable {
    IPlayerShopRepository shopRepository;

    [Inject]
    public UpgradePlayerSkillUseCaseImpl(IPlayerShopRepository shopRepository) {
        this.shopRepository = shopRepository;
    }

    public void Initialize() {
        Debug.Log("UpgradePlayerSkillUseCase Initialized");
    }

    public Task Invoke(string playerSkillId) {
        return shopRepository.UpgradePlayerSkill(playerSkillId);
    }
}