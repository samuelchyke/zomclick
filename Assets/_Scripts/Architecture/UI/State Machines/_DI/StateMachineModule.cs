using Zenject;

public class StateMachineModule : Installer<StateMachineModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<GameStateManager>().AsSingle().Lazy();

        Container.BindInterfacesAndSelfTo<PlayerStateManager>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<PlayerStateFactory>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<Projectile>().AsTransient().Lazy();
        
        Container.BindInterfacesAndSelfTo<AllyStateManager>().AsTransient().Lazy();
        Container.BindInterfacesAndSelfTo<AllyStateFactory>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<AllySpawnManager>().AsSingle().Lazy();
        // Container.BindInterfacesAndSelfTo<AutoAimProjectile>().AsTransient().Lazy();

        Container.BindInterfacesAndSelfTo<EnemyStateManager>().AsTransient().Lazy();
        Container.BindInterfacesAndSelfTo<EnemyStateFactory>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<EnemySpawnManager>().AsSingle().Lazy();

        Container.BindInterfacesAndSelfTo<BossStateManager>().AsTransient().Lazy();
        Container.BindInterfacesAndSelfTo<BossStateFactory>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<BossSpawnManager>().AsSingle().Lazy();

        Container.BindInterfacesAndSelfTo<ShopStateManager>().AsSingle().Lazy();
        Container.BindInterfacesAndSelfTo<ShopStateFactory>().AsSingle().Lazy();
    }
}