using Com.Studio.Zomclick.Assets.Scripts.Data.Dao.DI;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.DI;
using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories.DI;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.DI;
using Com.Studio.Zomclick.Assets.Scripts.UI.ViewModel.DI;
using Com.Studio.Zomclick.Assets.Scripts.UI.Events.DI;
using Com.Studio.Zomclick.Assets.Scripts.UI.StateMachines.DI;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts._ZenjectDI {
    public class ZenjectDI : MonoInstaller
    {
        public override void InstallBindings()
        {
            DatabaseModule.Install(Container);
            // SeedingModule.Install(Container);
            DaoModule.Install(Container);
            RepositoryModule.Install(Container);
            UseCaseModule.Install(Container);
            EventsModule.Install(Container);
            ViewModelModule.Install(Container);
            StateMachineModule.Install(Container);
        }
    }
}