using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Game.DI {
    public class GameUseCaseModule : Installer<GameUseCaseModule>
    {
        public override void InstallBindings()
        {

            Container.BindInterfacesAndSelfTo<IncrementRoundUseCaseImpl>()
                .FromMethod( ctx =>
                    {
                        return new IncrementRoundUseCaseImpl(
                            gameRepository: ctx.Container.Resolve<GameRepositoryImpl>()
                        );
                    })
                .AsSingle()
                .NonLazy();
        }
    }
}