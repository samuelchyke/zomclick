using Zenject;

public class UseCaseModule : Installer<UseCaseModule>
{
    public override void InstallBindings()
    {
        GameUseCaseModule.Install(Container);
        PlayerUseCaseModule.Install(Container);
        PlayerUpgradeShopUseCaseModule.Install(Container);
        AllyUseCaseModule.Install(Container);
        EnemyUseCaseModule.Install(Container);
    }
}