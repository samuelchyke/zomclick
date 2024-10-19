using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Models;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Enemy {
    public interface IReadEnemyWaveDetailsUseCase {
        Task<EnemyWaveDetails> Invoke();
    }

    public class ReadEnemyWaveDetailsUseCaseImpl : IReadEnemyWaveDetailsUseCase, IInitializable
    {
        IEnemyRepository enemyRepository;

        [Inject]
        public ReadEnemyWaveDetailsUseCaseImpl(IEnemyRepository enemyRepository)
        {
            this.enemyRepository = enemyRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadEnemyWaveDetailsUseCase Initialized");
        }

        public Task<EnemyWaveDetails> Invoke()
        {
            return enemyRepository.ReadEnemyWaveDetails();
        }
    }
}
