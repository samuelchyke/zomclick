using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;
using Debug = UnityEngine.Debug;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact {
    public interface IReadArtifactShopDetailsUseCase {
        public Task<ArtifactShopDetails> Invoke();
    }

    public class ReadArtifactShopDetailsUseCaseImpl : IReadArtifactShopDetailsUseCase, IInitializable
    {
        readonly IArtifactRepository artifactRepository;

        [Inject]
        public ReadArtifactShopDetailsUseCaseImpl(IArtifactRepository artifactRepository)
        {
            this.artifactRepository = artifactRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadArtifactShopDetailsUseCase Initialized");
        }

        public Task<ArtifactShopDetails> Invoke()
        {
            return artifactRepository.ReadArtifactShopDetails();
        }
    }
}