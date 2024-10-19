using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerUpgradeShop {
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
}