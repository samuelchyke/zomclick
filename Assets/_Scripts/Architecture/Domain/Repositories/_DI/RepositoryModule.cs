using Zenject;

public class RepositoryModule : Installer<RepositoryModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerUpgradeShopRepositoryImpl>()
            .FromMethod( ctx =>
                {
                    return new PlayerUpgradeShopRepositoryImpl(
                        shopDao: ctx.Container.Resolve<PlayerUpgradeShopDaoImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<PlayerRepositoryImpl>()
            .FromMethod( ctx =>
                {
                    return new PlayerRepositoryImpl(
                        playerStatsDao: ctx.Container.Resolve<PlayerStatsDaoImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<EnemyRepositoryImpl>()
            .FromMethod( ctx =>
                {
                    return new EnemyRepositoryImpl(
                        enemyDao: ctx.Container.Resolve<EnemyDaoImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<AllyRepositoryImpl>()
            .FromMethod(ctx => 
            {
                return new AllyRepositoryImpl(
                    allyDao: ctx.Container.Resolve<AllyDaoImpl>()
                );
            })
            .AsSingle()
            .NonLazy();
    }
}