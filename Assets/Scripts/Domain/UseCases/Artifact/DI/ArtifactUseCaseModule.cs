using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact.DI {
    public class ArtifactUseCaseModule : Installer<ArtifactUseCaseModule>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ReadArtifactShopDetailsUseCaseImpl>()
                .FromMethod( ctx =>
                    {
                        return new ReadArtifactShopDetailsUseCaseImpl(
                            artifactRepository: ctx.Container.Resolve<ArtifactRepositoryImpl>()
                        );
                    })
                .AsSingle()
                .NonLazy();
            

            Container.BindInterfacesAndSelfTo<ReadArtifactUseCaseImpl>()
                .FromMethod( ctx =>
                    {
                        return new ReadArtifactUseCaseImpl(
                            artifactRepository: ctx.Container.Resolve<ArtifactRepositoryImpl>()
                        );
                    })
                .AsSingle()
                .NonLazy();


            Container.BindInterfacesAndSelfTo<UnlockArtifactUseCaseImpl>()
                .FromMethod( ctx =>
                    {
                        return new UnlockArtifactUseCaseImpl(
                            artifactRepository: ctx.Container.Resolve<ArtifactRepositoryImpl>()
                        );
                    })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<UpgradeArtifactUseCaseImpl>()
                .FromMethod( ctx =>
                    {
                        return new UpgradeArtifactUseCaseImpl(
                            artifactRepository: ctx.Container.Resolve<ArtifactRepositoryImpl>()
                        );
                    })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ReadUnlockedArtifactsUseCaseImpl>()
                .FromMethod( ctx =>
                    {
                        return new ReadUnlockedArtifactsUseCaseImpl(
                            artifactRepository: ctx.Container.Resolve<ArtifactRepositoryImpl>()
                        );
                    })
                .AsSingle()
                .NonLazy();
        }
    }
}