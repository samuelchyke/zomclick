using Zenject;

// public class SeedingModule : Installer<SeedingModule>
// {
    // public override void InstallBindings()
    // {
    //     Container.BindInterfacesAndSelfTo<SeedDaoImpl>().FromMethod( ctx =>
    //     {
    //         return new SeedDaoImpl(
    //             database : ctx.Container.Resolve<DatabaseManager>().dbConnection
    //         );
    //     }).AsSingle().NonLazy();

    //     Container.BindInterfacesAndSelfTo<SeedEntityUpdaterImpl>().FromMethod( ctx =>
    //     {
    //         return new SeedEntityUpdaterImpl(
    //             database : ctx.Container.Resolve<DatabaseManager>().dbConnection
    //         );
    //     }).AsSingle().NonLazy();

    //     Container.BindInterfacesAndSelfTo<JsonSeeder>().FromMethod( ctx =>
    //     {
    //         return new JsonSeeder(
    //             seedDao : ctx.Container.Resolve<SeedDaoImpl>(),
    //             seedEntityUpdater : ctx.Container.Resolve<SeedEntityUpdaterImpl>()
    //         );
    //     }).AsSingle().NonLazy();
    // }
// }