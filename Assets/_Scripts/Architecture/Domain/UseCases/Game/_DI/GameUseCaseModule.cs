using Zenject;

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