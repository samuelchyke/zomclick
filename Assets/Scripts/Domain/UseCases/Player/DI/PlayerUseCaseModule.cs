using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Player.DI {
    public class PlayerUseCaseModule : Installer<PlayerUseCaseModule>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ReadPlayerStatsUseCaseImpl>()
                .FromMethod( ctx =>
                    {
                        return new ReadPlayerStatsUseCaseImpl(
                            playerRepository: ctx.Container.Resolve<PlayerRepositoryImpl>()
                        );
                    })
                .AsSingle()
                .NonLazy();
        }
    }
}