using Com.Studio.Zomclick.Assets.Scripts.Domain.Repositories;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Ally.DI {
    public class AllyUseCaseModule : Installer<AllyUseCaseModule>
    {
        public override void InstallBindings()
        {   
            Container.BindInterfacesAndSelfTo<ReadAlliesStatsUseCaseImpl>()
                .FromMethod(ctx =>
                {
                    return new ReadAlliesStatsUseCaseImpl(
                        allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                    );
                })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ReadAllyStatsUseCaseImpl>()
                .FromMethod(ctx =>
                {
                    return new ReadAllyStatsUseCaseImpl(
                        allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                    );
                })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<ReadAllySkillsUseCaseImpl>()
                .FromMethod(ctx =>
                {
                    return new ReadAllySkillsUseCaseImpl(
                        allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                    );
                })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<UnlockAllyUseCaseImpl>()
                .FromMethod(ctx =>
                {
                    return new UnlockAllyUseCaseImpl(
                        allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                    );
                })
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<UpgradeAllyStatsUseCaseImpl>()
            .FromMethod(ctx =>
            {
                return new UpgradeAllyStatsUseCaseImpl(
                    allyRepository: ctx.Container.Resolve<AllyRepositoryImpl>()
                );
            })
            .AsSingle()
            .NonLazy();
        }
    }
}