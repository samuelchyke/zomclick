using Zenject;

public class UseCaseModule : Installer<UseCaseModule>
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

        Container.BindInterfacesAndSelfTo<UpdatePlayerStatsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new UpdatePlayerStatsUseCaseImpl(
                        playerRepository: ctx.Container.Resolve<PlayerRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadEnemyStatsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new ReadEnemyStatsUseCaseImpl(
                        enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<UpdateEnemyStatsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new UpdateEnemyStatsUseCaseImpl(
                        enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadEnemyWaveDetailsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new ReadEnemyWaveDetailsUseCaseImpl(
                    enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<UpdateEnemyWaveDetailsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new UpdateEnemyWaveDetailsUseCaseImpl(
                    enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ObservePlayerUpgradeShopDetailsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new ObservePlayerUpgradeShopDetailsUseCaseImpl(
                        shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadShopDetailsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new ReadShopDetailsUseCaseImpl(
                        shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<AddEnemyGoldUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new AddEnemyGoldUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<UpdateShopDetailsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new UpdateShopDetailsUseCaseImpl(
                        shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadBossStatsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new ReadBossStatsUseCaseImpl(
                    enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<UpdateBossStatsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new UpdateBossStatsUseCaseImpl(
                    enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();
        
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