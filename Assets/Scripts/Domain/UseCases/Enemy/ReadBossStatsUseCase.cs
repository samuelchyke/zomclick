using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
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