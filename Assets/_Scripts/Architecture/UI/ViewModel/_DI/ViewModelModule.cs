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
                    upgradePlayerStatsUseCase: ctx.Container.Resolve<UpgradePlayerStatsUseCaseImpl>(),
                    unlockPlayerSkillUseCase: ctx.Container.Resolve<UnlockPlayerSkillUseCaseImpl>(),
                    upgradePlayerSkillUseCase: ctx.Container.Resolve<UpgradePlayerSkillUseCaseImpl>(),
                    eventsManager: ctx.Container.Resolve<EventsManager>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<AllyShopViewModelImpl>()
        .FromMethod( ctx =>
            {
                return new AllyShopViewModelImpl(
                    readAlliesStatsUseCase : ctx.Container.Resolve<ReadAlliesStatsUseCaseImpl>(),
                    readAllyStatsUseCase : ctx.Container.Resolve<ReadAllyStatsUseCaseImpl>(),
                    readAllySkillsUseCase : ctx.Container.Resolve<ReadAllySkillsUseCaseImpl>(),
                    unlockAllyUseCase : ctx.Container.Resolve<UnlockAllyUseCaseImpl>(),
                    upgradeAllyStatsUseCase : ctx.Container.Resolve<UpgradeAllyStatsUseCaseImpl>(),
                    eventsManager : ctx.Container.Resolve<EventsManager>()
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

        // Container.Bind<IAllyViewModel>()
        //     .FromMethod(ctx =>
        //     {
        //         return new AllyViewModelImpl(
        //             readAllyStatsUseCase: ctx.Container.Resolve<IReadAllyStatsUseCase>(),
        //             eventsManager: ctx.Container.Resolve<EventsManager>()
        //             // allyId: "john_id"
        //         );
        //     })
        //     .AsTransient()
        //     // .WithArguments(allyId)
        //     .NonLazy();

        BindAllyViewModel("john_id");
    }

    private void BindAllyViewModel(string allyId)
    {
        Container.Bind<IAllyViewModel>()
            .FromMethod(ctx =>
            {
                return new AllyViewModelImpl(
                    readAllyStatsUseCase: ctx.Container.Resolve<IReadAllyStatsUseCase>(),
                    eventsManager: ctx.Container.Resolve<EventsManager>(),
                    allyId: allyId 
                );
            })
            .AsTransient()
            // .WithArguments("allyId", allyId)
            .NonLazy();
    }
}