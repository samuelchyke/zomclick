using Zenject;

public class EventsModule : Installer<EventsModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<EventsManager>().AsSingle().NonLazy();
    }
}