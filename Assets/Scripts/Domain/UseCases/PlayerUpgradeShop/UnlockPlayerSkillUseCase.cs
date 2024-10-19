using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerUpgradeShop {
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
}