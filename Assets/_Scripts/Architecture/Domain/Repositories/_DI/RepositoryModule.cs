using Zenject;

public class RepositoryModule : Installer<RepositoryModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerShopRepositoryImpl>()
            .FromMethod( ctx =>
                {
                    return new PlayerShopRepositoryImpl(
                        playerShopDao: ctx.Container.Resolve<PlayerShopDaoImpl>(),
                        playerStatsDao: ctx.Container.Resolve<PlayerDaoImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerRepositoryImpl>()
            .FromMethod( ctx =>
                {
                    return new PlayerRepositoryImpl(
                        playerStatsDao: ctx.Container.Resolve<PlayerDaoImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<EnemyRepositoryImpl>()
            .FromMethod( ctx =>
                {
                    return new EnemyRepositoryImpl(
                        playerStatsDao: ctx.Container.Resolve<PlayerDaoImpl>(),
                        enemyDao: ctx.Container.Resolve<EnemyDaoImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameRepositoryImpl>()
            .FromMethod( ctx =>
                {
                    return new GameRepositoryImpl(
                        playerStatsDao: ctx.Container.Resolve<PlayerDaoImpl>(),
                        enemyDao: ctx.Container.Resolve<EnemyDaoImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<AllyRepositoryImpl>()
            .FromMethod(ctx => 
            {
                return new AllyRepositoryImpl(
                    playerDao : ctx.Container.Resolve<PlayerDaoImpl>(),
                    allyDao: ctx.Container.Resolve<AllyDaoImpl>()
                );
            })
            .AsSingle()
            .NonLazy();
    }
}