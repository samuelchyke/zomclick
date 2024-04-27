using Zenject;

public class PresenterModule : Installer<PresenterModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ShopPresenter>().AsSingle();
    }
}