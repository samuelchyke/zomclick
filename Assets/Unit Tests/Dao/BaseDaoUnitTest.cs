using SQLite4Unity3d;
using NUnit.Framework;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database.Entities;
using Com.Studio.Zomclick.Assets.Scripts.Data.Database;
using System.Threading.Tasks;

internal abstract class BaseDaoUnitTest
{
    // In-memory SQLite database for testing
    protected SQLiteConnection dbConnection { get; private set; }
    protected AppDatabaseImpl database;

    [SetUp]
    public void SetUpDatabase()
    {
        dbConnection = new SQLiteConnection(":memory:", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        var databaseManager = new AppDatabaseImpl();
        databaseManager.InitializeInMemory(dbConnection);
        database = databaseManager;     
    }

    [TearDown]
    public void ClearDatabase()
    {
        dbConnection.Close();
    }
}