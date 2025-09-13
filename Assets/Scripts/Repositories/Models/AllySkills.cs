namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IAllySkill
    {
        string id { get; init; }
        string allyId { get; init; }
        bool isUnlocked { get; init; }
        string description { get; init; }
        int unlockLevel { get; init; }
        int buff { get; init; }
    }

    public record AllySkill(
        string id,
        string allyId,
        bool isUnlocked,
        string description,
        int unlockLevel,
        int buff
    ) : IAllySkill;
}