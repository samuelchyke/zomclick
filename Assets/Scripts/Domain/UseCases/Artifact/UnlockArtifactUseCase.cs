using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact {
    public interface IUnlockArtifactUseCase {
        Task Invoke();
    }

    public class UnlockArtifactUseCaseImpl : IUnlockArtifactUseCase, IInitializable {
        IArtifactRepository artifactRepository;

        [Inject]
        public UnlockArtifactUseCaseImpl(IArtifactRepository artifactRepository) {
            this.artifactRepository = artifactRepository;
        }

        public void Initialize() {
            Debug.Log("UpgradeArtifactUseCaseImpl Initialized");
        }

        public Task Invoke() {
            return artifactRepository.UnlockArtifact();
        }
    }
}