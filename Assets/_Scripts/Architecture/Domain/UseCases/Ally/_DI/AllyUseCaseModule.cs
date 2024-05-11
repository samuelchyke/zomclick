using Zenject;

public class AllyUseCaseModule : Installer<AllyUseCaseModule>
{
    public override void InstallBindings()
    {   
        Container.BindInterfacesAndSelfTo<ReadAlliesStatsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new ReadAlliesStatsUseCaseImpl(
                    allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadAllyStatsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new ReadAllyStatsUseCaseImpl(
                    allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<UpdateAllyStatsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new UpdateAllyStatsUseCaseImpl(
                    allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();
    }
}