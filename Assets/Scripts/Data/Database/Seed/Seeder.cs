namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Seed {
    public interface ISeeder<T>
    {
        void Seed(T data);
    }
}