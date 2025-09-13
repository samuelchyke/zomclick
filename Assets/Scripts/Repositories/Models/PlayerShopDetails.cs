namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IPlayerShopDetails
    {
        string id { get; init; }
        int wallHealthCost { get; init; }
        int damageCost { get; init; }
        int critDamageCost { get; init; }
        int critRateCost { get; init; }
        int totalGold { get; init; }
    }

    public record PlayerShopDetails
    (
        string id,
        int wallHealthCost,
        int damageCost,
        int critDamageCost,
        int critRateCost,
        int totalGold
    )  : IPlayerShopDetails;
}