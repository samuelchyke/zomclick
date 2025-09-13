namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IPlayerSkill
    {
        string id { get; init; }
        bool isUnlocked { get; init; }
        int coolDown { get; init; }
        int level { get; init; }
        int unlockLevel { get; init; }
        int duration { get; init; }
        int buff { get; init; }
        int unlockCost { get; init; }
        int upgradeCost { get; init; }
        bool isActive { get; init; }
    }

    public record PlayerSkill(
        string id,
        bool isUnlocked,
        int coolDown,
        int level,
        int unlockLevel,
        int duration,
        int buff,
        int unlockCost,
        int upgradeCost,
        bool isActive
    ) : IPlayerSkill;
}