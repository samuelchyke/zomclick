namespace Com.Studio.Zomclick.Assets.Scripts.Domain.Models {
    public interface IAllySkill
    {
        string id { get; set; }
        string allyId { get; set; }
        bool isUnlocked { get; set; }
        string description { get; set; }
        int unlockLevel { get; set; }
        int buff { get; set; }
    }

    public record AllySkill : IAllySkill
    {
        public string id { get; set; }
        public string allyId { get; set; }
        public bool isUnlocked { get; set; }
        public string description { get; set; }
        public int unlockLevel { get; set; }
        public int buff { get; set; }
    }
}