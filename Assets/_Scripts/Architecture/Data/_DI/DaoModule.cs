using Zenject;

public class DaoModule : Installer<DaoModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerStatsDaoImpl>().FromMethod( ctx =>
        {
            return new PlayerStatsDaoImpl(
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