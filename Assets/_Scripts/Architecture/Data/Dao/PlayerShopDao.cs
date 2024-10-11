using System;
using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using R3;
using System.Linq;

public interface IPlayerShopDao 
{
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
        return Task.Run(() =>
        {
            string query = "SELECT * FROM playerShopDetails LIMIT 1";
            return _db.Query<PlayerShopEntity>(query).First();
        });
    }

    public Task UpdateShopDetails(PlayerShopEntity shopEntity)
    {
        return Task.Run(() => _db.Update(shopEntity));
    }
}
