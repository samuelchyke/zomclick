using Zenject;

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