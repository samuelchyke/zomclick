using System.Threading.Tasks;
using SQLite4Unity3d;
using Zenject;
using Debug = UnityEngine.Debug;
using System.Linq;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using System.Collections.Generic;

namespace Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao
{
    public interface IArtifactDao 
    {
        Task<ArtifactEntity> ReadRandomArtifact();
        Task<ArtifactEntity> ReadArtifact(string artifactId);
        Task<List<ArtifactEntity>> ReadUnlockedArtifacts();
        Task<ArtifactShopEntity> ReadArtifactShop();
        public Task UpdateArtifact(ArtifactEntity artifactEntity);
        public Task UpdateArtifactShop(ArtifactShopEntity artifactShopEntity);
    }

    public class ArtifactDaoImpl : IArtifactDao, IInitializable
    {

        SQLiteConnection _db;
        AppDatabaseImpl databaseManager;

        public ArtifactDaoImpl (SQLiteConnection databaseManager)
        {
            this._db = databaseManager;
        }

        public void Initialize()
        {
            // _db = databaseManager.dbConnection;
            Debug.Log("Shop Dao Initialized");
        }

        public Task<ArtifactEntity> ReadArtifact(string artifactId)
        {
            return Task.Run(() =>
            {
                string query = "SELECT * FROM artifacts WHERE id = ?";
                return _db.Query<ArtifactEntity>(query, artifactId).First();
            });
        }

        public Task<List<ArtifactEntity>> ReadUnlockedArtifacts()
        {
            return Task.Run(() =>
            {
                string query = "SELECT * FROM artifacts WHERE isUnlocked = 1";
                return _db.Query<ArtifactEntity>(query);
            });
        }

        public Task<ArtifactShopEntity> ReadArtifactShop()
        {
            return Task.Run(() =>
            {
                string query = "SELECT * FROM artifactShop LIMIT 1";
                return _db.Query<ArtifactShopEntity>(query).First();
            });
        }

        public Task<ArtifactEntity> ReadRandomArtifact()
        {
            return Task.Run(() =>
            {
                string query = "SELECT * FROM artifacts ORDER BY RANDOM() LIMIT 1";
                return _db.Query<ArtifactEntity>(query).FirstOrDefault();
            });
        }

        public Task UpdateArtifact(ArtifactEntity artifactEntity)
        {
            return Task.Run(() => _db.Update(artifactEntity));
        }

        public Task UpdateArtifactShop(ArtifactShopEntity artifactShopEntity)
        {
            return Task.Run(() => _db.Update(artifactShopEntity));
        }
    }
}