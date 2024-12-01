using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Enemy {
    public interface IReadBossStatsUseCase {
        Task<BossStats> Invoke();
    }

    public class ReadBossStatsUseCaseImpl : IReadBossStatsUseCase, IInitializable
    {
        IEnemyRepository enemyRepository;

        [Inject]
        public ReadBossStatsUseCaseImpl(IEnemyRepository enemyRepository)
        {
            this.enemyRepository = enemyRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadBossStatsUseCase Initialized");
        }

        public Task<BossStats> Invoke()
        {
            return enemyRepository.ReadBossStats();
        }
    }
}