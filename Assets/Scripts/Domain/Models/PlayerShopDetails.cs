namespace Com.Studio.Zomclick.Assets.Scripts.Domain.Models {
    public interface IPlayerShopDetails
    {
        string id { get; set; }
        int wallHealthCost { get; set; }
        int damageCost { get; set; }
        int critDamageCost { get; set; }
        int critRateCost { get; set; }
        int totalGold { get; set; }
    }

    public record PlayerShopDetails : IPlayerShopDetails
    {
        public string id { get; set; }
        public int wallHealthCost { get; set; }
        public int damageCost { get; set; }
        public int critDamageCost { get; set; }
        public int critRateCost { get; set; }
        public int totalGold { get; set; }
    }
}