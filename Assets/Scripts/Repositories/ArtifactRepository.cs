using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Builders;
using Com.Studio.Zomclick.Assets.Scripts.Repositories.Models;
using UnityEngine;

namespace Com.Studio.Zomclick.Assets.Scripts.Repositories {
    public interface IArtifactRepository
    {
        Task<ArtifactShopDetails> ReadArtifactShopDetails();
        Task<Artifact> ReadArtifact(string artifactId);
        Task<List<Artifact>> ReadUnlockedArtifacts();
        Task UpdateArtifact(string artifactId);
        Task UnlockArtifact();
    }

    public class ArtifactRepositoryImpl : IArtifactRepository
    {
        private readonly IArtifactDao artifactDao;
        private readonly IPlayerDao playerDao;

        public ArtifactRepositoryImpl(
            IArtifactDao artifactDao,
            IPlayerDao playerDao
        )
        {
            this.artifactDao = artifactDao;
            this.playerDao = playerDao;
        }

        public async Task<ArtifactShopDetails> ReadArtifactShopDetails()
        {
            var entity = await artifactDao.ReadArtifactShop();
            var playerStats = await playerDao.ReadPlayerStats();
            return new ArtifactShopDetailsBuilder().buildFrom(
                shopEntity: entity,
                totalRelics: playerStats.totalRelics
            );
        }

        public async Task<Artifact> ReadArtifact(string artifactId)
        {
            ArtifactEntity entity = await artifactDao.ReadArtifact(artifactId);
            return new ArtifactBuilder().buildFrom(entity);
        }

        public async Task<List<Artifact>> ReadUnlockedArtifacts()
        {
            var entities = await artifactDao.ReadUnlockedArtifacts();
            List<Artifact> artifacts = entities.Select(artifact => new ArtifactBuilder().buildFrom(artifact)).ToList();
            return artifacts;
        }

        public async Task UpdateArtifact(string artifactId)
        {
            ArtifactEntity entity = await artifactDao.ReadArtifact(artifactId);
            await artifactDao.UpdateArtifact(entity);
        }

        public async Task UnlockArtifact()
        {   
            var artifact = await artifactDao.ReadRandomArtifact();
            var artifactShop = await artifactDao.ReadArtifactShop();
            var playerStats = await playerDao.ReadPlayerStats();

            if (playerStats.totalRelics >= artifactShop.artifactUnlockCost)
            {
                artifact.isUnlocked = true;
                playerStats.totalRelics -= artifactShop.artifactUnlockCost;

                await artifactDao.UpdateArtifact(artifact);
                await artifactDao.UpdateArtifactShop(artifactShop);
                await playerDao.UpdatePlayerStats(playerStats);
            }
        }
    }
}