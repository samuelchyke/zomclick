using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Repositories;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories.DI {
    public class RepositoryModule : Installer<RepositoryModule>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerShopRepositoryImpl>()
                .FromMethod( ctx =>
                    {
                        return new PlayerShopRepositoryImpl(
                            playerShopDao: ctx.Container.Resolve<IPlayerShopDao>(),
                            playerStatsDao: ctx.Container.Resolve<IPlayerDao>()
                        );
                    })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerRepositoryImpl>()
                .FromMethod( ctx =>
                    {
                        return new PlayerRepositoryImpl(
                            playerStatsDao: ctx.Container.Resolve<IPlayerDao>()
                        );
                    })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<EnemyRepositoryImpl>()
                .FromMethod( ctx =>
                    {
                        return new EnemyRepositoryImpl(
                            playerStatsDao: ctx.Container.Resolve<IPlayerDao>(),
                            enemyDao: ctx.Container.Resolve<IEnemyDao>()
                        );
                    })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<GameRepositoryImpl>()
                .FromMethod( ctx =>
                    {
                        return new GameRepositoryImpl(
                            playerStatsDao: ctx.Container.Resolve<IPlayerDao>(),
                            enemyDao: ctx.Container.Resolve<IEnemyDao>()
                        );
                    })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<AllyRepositoryImpl>()
                .FromMethod(ctx => 
                {
                    return new AllyRepositoryImpl(
                        playerDao : ctx.Container.Resolve<IPlayerDao>(),
                        allyDao: ctx.Container.Resolve<IAllyDao>()
                    );
                })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerSkillsRepositoryImpl>()
                .FromMethod( ctx =>
                    {
                        return new PlayerSkillsRepositoryImpl(
                            playerStatsDao: ctx.Container.Resolve<IPlayerDao>()
                        );
                    })
                .AsSingle()
                .NonLazy();
        }
    }
}