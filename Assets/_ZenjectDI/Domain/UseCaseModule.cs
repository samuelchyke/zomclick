using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Ally.DI;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Artifact.DI;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Enemy.DI;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Game.DI;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.Player.DI;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerSkills;
using Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.PlayerUpgradeShop.DI;
using Zenject;

namespace Com.Studio.Zomclick.Assets.Scripts.Domain.UseCases.DI {
    public class UseCaseModule : Installer<UseCaseModule>
    {
        public override void InstallBindings()
        {
            GameUseCaseModule.Install(Container);
            PlayerUseCaseModule.Install(Container);
            PlayerSkillsUseCaseModule.Install(Container);
            PlayerUpgradeShopUseCaseModule.Install(Container);
            AllyUseCaseModule.Install(Container);
            EnemyUseCaseModule.Install(Container);
            ArtifactUseCaseModule.Install(Container);
        }
    }
}