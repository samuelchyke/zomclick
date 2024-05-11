using Zenject;

public class ViewModelModule : Installer<ViewModelModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerShopViewModelImpl>()
        .FromMethod( ctx =>
            {
                return new PlayerShopViewModelImpl(
                    readShopDetailsUseCase: ctx.Container.Resolve<ReadShopDetailsUseCaseImpl>(),
                    buyDamageUseCase: ctx.Container.Resolve<BuyDamageUseCaseImpl>(),
                    buyCritRateUseCase: ctx.Container.Resolve<BuyCritRateUseCaseImpl>(),
                    buyHealthUseCase: ctx.Container.Resolve<BuyHealthUseCaseImpl>(),
                    buyCritDamageUseCase: ctx.Container.Resolve<BuyCritDamageUseCaseImpl>(),
                    eventsManager: ctx.Container.Resolve<EventsManager>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<EnemyViewModelImpl>()
        .FromMethod( ctx =>
            {
                return new EnemyViewModelImpl(
                    readEnemyStatsUseCase: ctx.Container.Resolve<ReadEnemyStatsUseCaseImpl>(),
                    readEnemyWaveDetailsUseCase: ctx.Container.Resolve<ReadEnemyWaveDetailsUseCaseImpl>(),
                    onEnemyDeathUseCase: ctx.Container.Resolve<OnEnemyDeathUseCaseImpl>(),
                    eventsManager: ctx.Container.Resolve<EventsManager>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerViewModelImpl>()
        .FromMethod( ctx =>
            {
                return new PlayerViewModelImpl(
                    readPlayerStatsUseCase: ctx.Container.Resolve<ReadPlayerStatsUseCaseImpl>(),
                    eventsManager: ctx.Container.Resolve<EventsManager>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<GameViewModelImpl>()
        .FromMethod(ctx =>
        {
            return new GameViewModelImpl(
                readPlayerStatsUseCase: ctx.Container.Resolve<IReadPlayerStatsUseCase>(),
                readEnemyWaveDetailsUseCase: ctx.Container.Resolve<IReadEnemyWaveDetailsUseCase>(),
                incrementRoundUseCase: ctx.Container.Resolve<IIncrementRoundUseCase>(),
                eventsManager: ctx.Container.Resolve<EventsManager>()
            );
        })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<BossViewModelImpl>()
        .FromMethod(ctx =>
        {
            return new BossViewModelImpl(
                readPlayerStatsUseCase: ctx.Container.Resolve<IReadPlayerStatsUseCase>(),
                readBossStatsUseCase: ctx.Container.Resolve<IReadBossStatsUseCase>(),
                eventsManager: ctx.Container.Resolve<EventsManager>()
            );
        })
        .AsSingle()
        .NonLazy();
    }

}