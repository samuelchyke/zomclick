using Zenject;

public class ViewModelModule : Installer<ViewModelModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerUpgradeShopViewModelImpl>()
        .FromMethod( ctx =>
            {
                return new PlayerUpgradeShopViewModelImpl(
                    readEnemyStatsUseCase: ctx.Container.Resolve<ReadEnemyStatsUseCaseImpl>(),
                    readBossStatsUseCase: ctx.Container.Resolve<ReadBossStatsUseCaseImpl>(),
                    readPlayerStatsUseCase: ctx.Container.Resolve<ReadPlayerStatsUseCaseImpl>(),
                    updatePlayerStatsUseCase: ctx.Container.Resolve<UpdatePlayerStatsUseCaseImpl>(),
                    updateShopDetailsUseCase: ctx.Container.Resolve<UpdateShopDetailsUseCaseImpl>(),
                    readShopDetailsUseCaseUseCase: ctx.Container.Resolve<ReadShopDetailsUseCaseImpl>(),
                    eventsManager: ctx.Container.Resolve<EventsManager>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<EnemyViewModelImpl>()
        .FromMethod( ctx =>
            {
                return new EnemyViewModelImpl(
                    readPlayerStatsUseCase: ctx.Container.Resolve<ReadPlayerStatsUseCaseImpl>(),
                    readEnemyStatsUseCase: ctx.Container.Resolve<ReadEnemyStatsUseCaseImpl>(),
                    updateEnemyStatsUseCase: ctx.Container.Resolve<UpdateEnemyStatsUseCaseImpl>(),
                    readEnemyWaveDetailsUseCase: ctx.Container.Resolve<ReadEnemyWaveDetailsUseCaseImpl>(),
                    updateEnemyWaveDetailsUseCase: ctx.Container.Resolve<UpdateEnemyWaveDetailsUseCaseImpl>(),
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
                    updatePlayerStatsUseCase: ctx.Container.Resolve<UpdatePlayerStatsUseCaseImpl>(),
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
                updateBossStatsUseCase: ctx.Container.Resolve<IUpdateBossStatsUseCase>(),
                eventsManager: ctx.Container.Resolve<EventsManager>()
            );
        })
        .AsSingle()
        .NonLazy();
    }

}