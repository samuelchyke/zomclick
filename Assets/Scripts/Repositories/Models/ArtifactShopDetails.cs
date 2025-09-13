namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IArtifactShopDetails
    {
        string id { get; init; }
        public int artifactUnlockCost { get; init; }
        public int totalRelics { get; init; }

    }

    public record ArtifactShopDetails(
        string id,
        int artifactUnlockCost,
        int totalRelics
    ) : IArtifactShopDetails;
}