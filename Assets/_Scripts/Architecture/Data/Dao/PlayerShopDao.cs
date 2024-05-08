using System;
using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using R3;

public interface IPlayerShopDao 
{
    Observable<PlayerShopEntity> ObserveShopDetails();

    ReactiveProperty<PlayerShopEntity> ObservShopDetails();


    Task<PlayerShopEntity> ObserveShopDetails(PlayerShopEntity shopEntity);


    Task<PlayerShopEntity> ReadShopDetails();

    public Task UpdateShopDetails(PlayerShopEntity shopEntity);
}

public class PlayerShopDaoImpl : IPlayerShopDao, IInitializable
{

    SQLiteConnection _db;
    DatabaseManager databaseManager;

    [Inject]
    public PlayerShopDaoImpl (DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }


    public void Initialize()
    {
        _db = databaseManager.dbConnection;
        Debug.Log("Shop Dao Initialized");
    }

    public Task<PlayerShopEntity> ReadShopDetails()
    {
        var shopEntity = _db.Table<PlayerShopEntity>().First();
        Debug.Log("Player Dao - Read Player Stats: " + shopEntity.id);
        return Task.Run(() => shopEntity);
    }

    public ReactiveProperty<PlayerShopEntity> ObservShopDetails()
    {
        var shopEntity = _db.Table<PlayerShopEntity>().First();

        return new ReactiveProperty<PlayerShopEntity> { Value = shopEntity };
    }

    public Observable<PlayerShopEntity> ObserveShopDetails()
    {
        return Observable.Create<PlayerShopEntity>(observer =>
    {
        // Attempt to fetch the initial state from the database
        PlayerShopEntity currentEntity = _db.Table<PlayerShopEntity>().FirstOrDefault();
        
        // Check if the entity is found
        if (currentEntity != null)
        {
            // If entity is found, notify the observer
            observer.OnNext(currentEntity);
            observer.OnCompleted(); // Complete the observable after sending the initial state
        }

        // Return a disposable that does nothing (since there are no resources to dispose of)
        return Disposable.Empty;
    });
    }

    public Task UpdateShopDetails(PlayerShopEntity shopEntity)
    {
        return Task.Run(() => _db.Update(shopEntity));
        // return ReadShopDetails();
    }

    public Task<PlayerShopEntity> ObserveShopDetails(PlayerShopEntity shopEntity)
    {
        Task.Run(() => _db.Update(shopEntity));
        return Task.Run(() =>_db.Table<PlayerShopEntity>().FirstOrDefault());
    }

}
