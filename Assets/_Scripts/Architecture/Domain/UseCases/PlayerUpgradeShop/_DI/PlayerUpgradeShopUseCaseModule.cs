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

        Container.BindInterfacesAndSelfTo<BuyDamageUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new BuyDamageUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<BuyCritRateUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new BuyCritRateUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<BuyCritDamageUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new BuyCritDamageUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<BuyHealthUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new BuyHealthUseCaseImpl(
                    shopRepository: ctx.Container.Resolve<PlayerShopRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();
    }
}