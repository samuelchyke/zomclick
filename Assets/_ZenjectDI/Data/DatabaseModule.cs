using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.DI {
    public class DatabaseModule : Installer<DatabaseModule>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AppDatabaseImpl>().AsSingle().NonLazy();
            // Container.Bind<IAppDatabase>().To<AppDatabaseImpl>().AsSingle().NonLazy();
        }
    }
}