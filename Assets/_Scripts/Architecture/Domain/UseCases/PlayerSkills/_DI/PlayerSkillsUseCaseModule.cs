using Zenject;

public class PlayerSkillsUseCaseModule : Installer<PlayerSkillsUseCaseModule>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ReadPlayerSkillUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new ReadPlayerSkillUseCaseImpl(
                    playerSkillsRepository: ctx.Container.Resolve<PlayerSkillsRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<ReadPlayerSkillsUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new ReadPlayerSkillsUseCaseImpl(
                    playerSkillRepository: ctx.Container.Resolve<PlayerSkillsRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<ToggleSkillActiveUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new ToggleSkillActiveUseCaseImpl(
                    playerSkillsRepository: ctx.Container.Resolve<PlayerSkillsRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();

        Container.BindInterfacesAndSelfTo<IncreasePlayerGoldUseCaseImpl>()
        .FromMethod( ctx =>
            {
                return new IncreasePlayerGoldUseCaseImpl(
                    playerSkillsRepository: ctx.Container.Resolve<PlayerSkillsRepositoryImpl>()
                );
            })
        .AsSingle()
        .NonLazy();
    }
}