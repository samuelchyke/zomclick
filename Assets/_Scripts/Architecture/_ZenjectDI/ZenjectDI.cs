using Zenject;

public class ZenjectDI : MonoInstaller
{
    public override void InstallBindings()
    {
        DatabaseModule.Install(Container);
        DaoModule.Install(Container);
        RepositoryModule.Install(Container);
        UseCaseModule.Install(Container);
        EventsModule.Install(Container);
        ViewModelModule.Install(Container);
        PresenterModule.Install(Container);
        StateMachineModule.Install(Container);
    }
}