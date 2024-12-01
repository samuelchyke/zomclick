using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.UI.Events.DI {
    public class EventsModule : Installer<EventsModule>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EventsManager>().AsSingle().NonLazy();
        }
    }
}