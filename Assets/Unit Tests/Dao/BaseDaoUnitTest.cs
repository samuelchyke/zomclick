// using SQLite4Unity3d;
// using NUnit.Framework;

// internal abstract class BaseDaoUnitTest
// {
//     protected SQLiteConnection database { get; private set; }

//     [SetUp]
//     public void SetUpDatabase()
//     {
//         // Create an in-memory SQLite database
//         database = new SQLiteConnection(":memory:", SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
//         InitializeDatabase();
//     }

//     [TearDown]
//     public void ClearDatabase()
//     {
//         // Close the in-memory database
//         database.Close();
//     }

//     // Method to create necessary tables for unit testing, to be overridden by derived classes
//     protected virtual void InitializeDatabase()
//     {
//         database.CreateTable<MetadataEntity>();
//         database.CreateTable<PlayerStatsEntity>();
//         database.CreateTable<PlayerSkillEntity>();
//         database.CreateTable<EnemyStatsEntity>();
//         database.CreateTable<EnemyWaveEntity>();
//         database.CreateTable<BossStatsEntity>();
//         database.CreateTable<PlayerShopEntity>();
//         database.CreateTable<AllyStatsEntity>();
//         database.CreateTable<AllySkillEntity>();
//     }
// }
