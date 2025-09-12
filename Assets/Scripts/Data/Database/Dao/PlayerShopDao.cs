using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using R3;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao
{
    public interface IPlayerShopDao 
    {
        Observable<PlayerShopEntity> ObserveShopDetails();
        Task<PlayerShopEntity> ReadShopDetails();
        public Task UpdateShopDetails(PlayerShopEntity shopEntity);
    }

    public class PlayerShopDaoImpl : IPlayerShopDao, IInitializable
    {
        readonly SQLiteConnection _db;
        private BehaviorSubject<PlayerShopEntity> _playerShop;

        public PlayerShopDaoImpl(SQLiteConnection databaseManager)
        {
            _db = databaseManager;
            _ = InitializeObservables();
        }

        private async Task InitializeObservables()
        {
            var entity = await ReadShopDetails();
            _playerShop = new BehaviorSubject<PlayerShopEntity>(entity);
        }

        public Observable<PlayerShopEntity> ObserveShopDetails()
        {
            return _playerShop;
        }

        public void Initialize()
        {
            // _db = databaseManager.dbConnection;
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

        public async Task UpdateShopDetails(PlayerShopEntity shopEntity)
        {
            await Task.Run(() => _db.Update(shopEntity)).ConfigureAwait(false);
            _playerShop.OnNext(await ReadShopDetails().ConfigureAwait(false));
        }
    }
}