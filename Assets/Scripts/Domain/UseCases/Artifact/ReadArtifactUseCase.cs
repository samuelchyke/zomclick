using System.Threading.Tasks;
using Zenject;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Models = Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using System.Collections.Generic;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact {
    public interface IReadArtifactUseCase {
        public Task<Models.Artifact> Invoke(string artifactId);
    }

    public class ReadArtifactUseCaseImpl : IReadArtifactUseCase, IInitializable
    {
        readonly IArtifactRepository artifactRepository;

        [Inject]
        public ReadArtifactUseCaseImpl(IArtifactRepository artifactRepository)
        {
            this.artifactRepository = artifactRepository;
        }

        public void Initialize()
        {
            Debug.Log("ReadArtifactUseCaseImpl Initialized");
        }

        public Task<Models.Artifact> Invoke(string artifactId)
        {
            return artifactRepository.ReadArtifact(artifactId);
        }
    }
}