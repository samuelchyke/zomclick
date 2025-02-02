using System;
using System.Collections.Generic;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories.Models {
    public interface IArtifactShopDetails
    {
        string id { get; set; }
        public int artifactUnlockCost { get; set; }
        public int totalRelics { get; set; }

    }

    public record ArtifactShopDetails : IArtifactShopDetails
    {
        public string id { get; set; }
        public int artifactUnlockCost { get; set; }
        public int totalRelics { get; set; }
    }
}