using System.Threading.Tasks;
using Zenject;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Models = Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using System.Collections.Generic;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact {
    public interface IReadUnlockedArtifactsUseCase {
        public Task<List<Models.Artifact>> Invoke();
    }

    public class ReadUnlockedArtifactsUseCaseImpl : IReadUnlockedArtifactsUseCase, IInitializable
    {
        readonly IArtifactRepository artifactRepository;

        [Inject]
        public ReadUnlockedArtifactsUseCaseImpl(IArtifactRepository artifactRepository)
        {
            this.artifactRepository = artifactRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadUnlockedArtifactsUseCaseImpl Initialized");
        }

        public Task<List<Models.Artifact>> Invoke()
        {
            return artifactRepository.ReadUnlockedArtifacts();
        }
    }
}