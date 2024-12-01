using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Dao;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Testing;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Com.Studio.Zomclick.Assets.UnitTests.Dao {
internal class Dao : BaseDaoUnitTest {
    private IPlayerShopDao _playerShopDao;
    private IStubDao stubDao;


    [SetUp]
    public void SetUp()
    {
        SetUpDatabase();
        _playerShopDao = database.PlayerShopDao();
        stubDao = database.StubDao();
    }

    [TearDown]
    public void TearDown()
    {
        ClearDatabase();
    }

    /**
     * GIVEN
     *      A PlayerShopDao with a PlayerShopEntity already inserted into the database
     * WHEN
     *      reading the shop details
     * THEN
     *      the details of the inserted PlayerShopEntity should be returned
     */
    [Test]
    public async Task ReadShopDetails_ShouldReturnShopDetails()
    {
        // Arrange
        var shopEntity = new Stub().PlayerShopEntity();

        // Insert the shop entity into the database for testing
        await database.RunInTransaction(async () =>
        {
            await stubDao.InsertPlayerShop(shopEntity);
        });

        // Act
        var retrievedShopEntity = await _playerShopDao.ReadShopDetails();

        // Assert
        Assert.IsNotNull(retrievedShopEntity);
        Assert.AreEqual(shopEntity.id, retrievedShopEntity.id);
    }

//     /**
//      * GIVEN
//      *      A PlayerShopDao with an existing PlayerShopEntity in the database
//      * WHEN
//      *      updating the PlayerShopEntity's details
//      * THEN
//      *      the updated details should be reflected when reading the shop details
//      */
//     [Test]
//     public async Task UpdateShopDetails_ShouldUpdateShopDetails()
//     {
//         // Arrange
//         var shopEntity = new PlayerShopEntity
//         {
//             Id = "1",
//             Name = "Old Shop Name",
//             Currency = 500
//         };

//         // Insert the original entity into the database for testing
//         await database.RunInTransaction(async () =>
//         {
//             await _playerShopDao.InsertPlayerShop(shopEntity);
//         });

//         // Modify the shop details
//         shopEntity.Name = "New Shop Name";
//         shopEntity.Currency = 1000;

//         // Act
//         await _playerShopDao.UpdateShopDetails(shopEntity);
//         var updatedShopEntity = await _playerShopDao.ReadShopDetails();

//         // Assert
//         Assert.IsNotNull(updatedShopEntity);
//         Assert.AreEqual(shopEntity.Id, updatedShopEntity.Id);
//         Assert.AreEqual(shopEntity.Name, updatedShopEntity.Name);
//         Assert.AreEqual(shopEntity.Currency, updatedShopEntity.Currency);
//     }
}
}