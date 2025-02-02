using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact {
    public interface IUpgradeArtifactUseCase {
        Task Invoke(string artifactId);
    }

    public class UpgradeArtifactUseCaseImpl : IUpgradeArtifactUseCase, IInitializable {
        IArtifactRepository artifactRepository;

        [Inject]
        public UpgradeArtifactUseCaseImpl(IArtifactRepository artifactRepository) {
            this.artifactRepository = artifactRepository;
        }

        public void Initialize() {
            Debug.Log("UpgradeArtifactUseCaseImpl Initialized");
        }

        public Task Invoke(string artifactId) {
            return artifactRepository.UpdateArtifact(artifactId);
        }
    }
}