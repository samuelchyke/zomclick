using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Enemy {
    public interface IReadEnemyStatsUseCase {
        public Task<EnemyStats> Invoke();
    }

    public class ReadEnemyStatsUseCaseImpl : IReadEnemyStatsUseCase, IInitializable
    {
        IEnemyRepository enemyRepository;

        [Inject]
        public ReadEnemyStatsUseCaseImpl(IEnemyRepository enemyRepository)
        {
            this.enemyRepository = enemyRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadEnemyStatsUseCase Initialized");
        }

        public Task<EnemyStats> Invoke()
        {
            return enemyRepository.ReadEnemyStats();
        }
    }
}