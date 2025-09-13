using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders {
    public class ArtifactShopDetailsBuilder
    {
        public ArtifactShopDetails buildFrom(ArtifactShopEntity shopEntity, int totalRelics)
        {
            return new ArtifactShopDetails 
            (
                id: shopEntity.id,
                artifactUnlockCost: shopEntity.artifactUnlockCost,
                totalRelics: totalRelics
            );
        }
    }
}