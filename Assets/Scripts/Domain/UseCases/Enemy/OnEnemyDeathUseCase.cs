using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Enemy {
    public interface IOnEnemyDeathUseCase {
        public Task Invoke();
    }

    public class OnEnemyDeathUseCaseImpl : IOnEnemyDeathUseCase, IInitializable
    {
        IEnemyRepository enemyRepository;

        [Inject]
        public OnEnemyDeathUseCaseImpl(IEnemyRepository enemyRepository)
        {
            this.enemyRepository = enemyRepository;
        }

        public void Initialize()
        {
            Debug.Log("OnEnemyDeathUseCaseImpl Initialized");
        }

        public async Task Invoke()
        {
            await enemyRepository.OnEnemyDeath();
        }
    }
}