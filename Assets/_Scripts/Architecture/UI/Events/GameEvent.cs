public static class GameEvent {

    public static class GameViewModelEvent
    {
        const string eventId = "GameViewModelEvent/";
        public const string GAME_OVER = eventId + "GAME_OVER";
        public const string START_NEXT_ROUND = eventId + "NEXT_ROUND";
        public const string START_BOSS_ROUND = eventId + "START_BOSS_ROUND";
        public const string RESTART_ROUND = eventId + "RESTART_ROUND";
    }


    public static class EnemyViewModelEvent
    {
        const string eventId = "EnemyViewModelEvent/";
        public const string ENEMY_VM_SETUP_COMPLETE = eventId + "ENEMY_VM_SETUP_COMPLETE";
        public const string UPDATE_ENEMY_STATS = eventId + "UPDATE_ENEMY_STATS";
        public const string UPDATE_ENEMY_STATS_MANAGER = eventId + "UPDATE_ENEMY_STATS_MANAGER";
        public const string UPDATE_ENEMY_WAVE_DETAILS = eventId + "UPDATE_ENEMY_WAVE_DETAILS";
        public const string INFLICT_DAMAGE = eventId + "INFLICT_DAMAGE";
        public const string ON_DEATH = eventId + "ON_DEATH";
    }

    public static class ShopViewModelEvent
    {
        const string eventId = "ShopViewModelEvent/";
        public const string SHOP_VM_SETUP_COMPLETE = eventId + "SHOP_VM_SETUP_COMPLETE";
        public const string UPDATE_SHOP_DETAILS = eventId + "UPDATE_SHOP_DETAILS";
        public const string UPDATE_PLAYER_STATS = eventId + "UPDATE_PLAYER_STATS";
        public const string UPDATE_TEXT = eventId + "UPDATE_TEXT";
    }

    public static class PlayerViewModelEvent
    {
        const string eventId = "PlayerViewModelEvent/";
        public const string SHOP_VM_SETUP_COMPLETE = eventId + "SHOP_VM_SETUP_COMPLETE";
        public const string UPDATE_SHOP_DETAILS = eventId + "UPDATE_SHOP_DETAILS";
        public const string UPDATE_PLAYER_STATS = eventId + "UPDATE_ENEMY_STATS";
        public const string UPDATE_ENEMY_STATS = eventId + "UPDATE_ENEMY_STATS";
        public const string INFLICT_DAMAGE = eventId + "INFLICT_DAMAGE";
    }

    public class BossViewModelEvent
    {
        public const string BOSS_VM_SETUP_COMPLETE = "BOSS_VM_SETUP_COMPLETE";
        public const string UPDATE_BOSS_STATS = "UPDATE_BOSS_STATS";
        public const string INFLICT_DAMAGE = "INFLICT_DAMAGE";
        public const string ON_DEATH = "ON_DEATH";
    }
}