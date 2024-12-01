using Zenject;
using SQLite4Unity3d;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Dao.DI {
    public class DaoModule : Installer<DaoModule>
    {
        public override void InstallBindings()
        {
            // Container.BindInterfacesAndSelfTo<SeedDaoImpl>().FromMethod( ctx =>
            // {
            //     return new SeedDaoImpl(
            //         db: ctx.Container.Resolve<DatabaseManager>()
            //     );
            // }).AsSingle().NonLazy();

            // Container.BindInterfacesAndSelfTo<ISeedEntityUpdater>().FromMethod( ctx =>
            // {
            //     return new SeedEntityUpdaterImpl(
            //         database: ctx.Container.Resolve<SQLiteConnection>()
            //     );
            // }).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<IPlayerDao>().FromMethod(ctx =>
            {
                return ctx.Container.Resolve<IAppDatabase>().PlayerDao();
            }).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<IEnemyDao>().FromMethod(ctx =>
            {
                return ctx.Container.Resolve<IAppDatabase>().EnemyDao();
            }).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<IPlayerShopDao>().FromMethod(ctx =>
            {
                return ctx.Container.Resolve<IAppDatabase>().PlayerShopDao();
            }).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<IAllyDao>().FromMethod(ctx =>
            {
                return ctx.Container.Resolve<IAppDatabase>().AllyDao();
            }).AsSingle().NonLazy();
        }
    }
}