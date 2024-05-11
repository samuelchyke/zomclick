using Zenject;

public class EnemyUseCaseModule : Installer<EnemyUseCaseModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ReadEnemyStatsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new ReadEnemyStatsUseCaseImpl(
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

        Container.BindInterfacesAndSelfTo<ReadBossStatsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new ReadBossStatsUseCaseImpl(
                        enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<OnEnemyDeathUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new OnEnemyDeathUseCaseImpl(
                    enemyRepository: ctx.Container.Resolve<EnemyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();
    }
}