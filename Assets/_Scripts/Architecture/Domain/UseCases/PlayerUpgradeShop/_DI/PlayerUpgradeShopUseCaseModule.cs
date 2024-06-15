using Zenject;

public class PlayerUpgradeShopUseCaseModule : Installer<PlayerUpgradeShopUseCaseModule>
{
    public override void InstallBindings()
    {

        Container.BindInterfacesAndSelfTo<ReadShopDetailsUseCaseImpl>()
            .FromMethod( ctx =>
                {
                    return new ReadShopDetailsUseCaseImpl(
                        shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                    );
                })
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<UpgradePlayerStatsUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new UpgradePlayerStatsUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadPlayerSkillUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new ReadPlayerSkillUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadPlayerSkillsUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new ReadPlayerSkillsUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<UnlockPlayerSkillUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new UnlockPlayerSkillUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<UpgradePlayerSkillUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new UpgradePlayerSkillUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();
    }
}