using Zenject;

public class DatabaseModule : Installer<DatabaseModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<DatabaseManager>().AsSingle().NonLazy();
    }
}