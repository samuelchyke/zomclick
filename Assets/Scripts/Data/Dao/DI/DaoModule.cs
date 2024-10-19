using Zenject;
using SQLite4Unity3d;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database;

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

            Container.BindInterfacesAndSelfTo<PlayerDaoImpl>().FromMethod( ctx =>
            {
                return new PlayerDaoImpl(
                    databaseManager : ctx.Container.Resolve<DatabaseManager>()
                );
            }).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<EnemyDaoImpl>().FromMethod( ctx =>
            {
                return new EnemyDaoImpl(
                    databaseManager : ctx.Container.Resolve<DatabaseManager>()
                );
            }).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<PlayerShopDaoImpl>().FromMethod( ctx =>
            {
                return new PlayerShopDaoImpl(
                    databaseManager : ctx.Container.Resolve<DatabaseManager>()
                );
            }).AsSingle().NonLazy();

            Container.BindInterfacesAndSelfTo<AllyDaoImpl>().FromMethod( ctx =>
            {
                return new AllyDaoImpl(
                    databaseManager : ctx.Container.Resolve<DatabaseManager>()
                );
            }).AsSingle().NonLazy();
        }
    }
}