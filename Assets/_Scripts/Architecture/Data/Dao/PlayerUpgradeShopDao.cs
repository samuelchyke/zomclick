using System;
using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using R3;

public interface IPlayerUpgradeShopDao 
{
    Observable<PlayerUpgradeShopEntity> ObserveShopDetails();

    ReactiveProperty<PlayerUpgradeShopEntity> ObservShopDetails();


    Task<PlayerUpgradeShopEntity> ObserveShopDetails(PlayerUpgradeShopEntity shopEntity);


    Task<PlayerUpgradeShopEntity> ReadShopDetails();

    public Task UpdateAndFetchShopDetails(PlayerUpgradeShopEntity shopEntity);
}

public class PlayerUpgradeShopDaoImpl : IPlayerUpgradeShopDao, IInitializable
{

    SQLiteConnection _db;
    DatabaseManager databaseManager;

    [Inject]
    public PlayerUpgradeShopDaoImpl (DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }


    public void Initialize()
    {
        _db = databaseManager.dbConnection;
        Debug.Log("Shop Dao Initialized");
    }

    public Task<PlayerUpgradeShopEntity> ReadShopDetails()
    {
        var shopEntity = _db.Table<PlayerUpgradeShopEntity>().First();
        Debug.Log("Player Dao - Read Player Stats: " + shopEntity.id);
        return Task.Run(() => shopEntity);
    }

    public ReactiveProperty<PlayerUpgradeShopEntity> ObservShopDetails()
    {
        var shopEntity = _db.Table<PlayerUpgradeShopEntity>().First();

        return new ReactiveProperty<PlayerUpgradeShopEntity> { Value = shopEntity };
    }

    public Observable<PlayerUpgradeShopEntity> ObserveShopDetails()
    {
        return Observable.Create<PlayerUpgradeShopEntity>(observer =>
    {
        // Attempt to fetch the initial state from the database
        PlayerUpgradeShopEntity currentEntity = _db.Table<PlayerUpgradeShopEntity>().FirstOrDefault();
        
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

    public Task UpdateAndFetchShopDetails(PlayerUpgradeShopEntity shopEntity)
    {
        return Task.Run(() => _db.Update(shopEntity));
        // return ReadShopDetails();
    }

    public Task<PlayerUpgradeShopEntity> ObserveShopDetails(PlayerUpgradeShopEntity shopEntity)
    {
                 Task.Run(() => _db.Update(shopEntity));
        return Task.Run(() =>_db.Table<PlayerUpgradeShopEntity>().FirstOrDefault());
    }

}
