using SQLite4Unity3d;

public class Migrations {

    private SQLiteConnection dbConnection;
    private int currentDbVersion;

    public Migrations(
        SQLiteConnection dbConnection,
        int currentDbVersion
    ) {
        this.dbConnection = dbConnection;
        this.currentDbVersion = currentDbVersion;
    }

    public void ApplyMigrations(bool applyMigrations) {
        if(!applyMigrations) return;
        var dbVersionInfo = dbConnection.Table<MetadataEntity>().FirstOrDefault();
        int dbVersion = dbVersionInfo != null ? dbVersionInfo.dataVersion : 1;

        while (dbVersion < currentDbVersion) 
        {
            switch (dbVersion)
            {
                case 2:
                    MigrateToVersion2();
                    break;
                case 3:
                    // migrations.MigrateToVersion3();
                    break;
                // Add cases as new versions come along
            }

            // After migration, update currentVersion to reflect the new database version
            dbVersionInfo = dbConnection.Table<MetadataEntity>().FirstOrDefault();
            dbVersion = dbVersionInfo != null ? dbVersionInfo.dataVersion : 0;
        }
    }

    public void MigrateToVersion2() {
        // Example migration: adding a new column to an existing table
        // dbConnection.Execute("ALTER TABLE PlayerStatsEntity ADD COLUMN NewColumn TEXT");

        // Update the version number in the database
        UpdateDatabaseVersion(2);
    }

    void UpdateDatabaseVersion(int newVersion){
        dbConnection.Execute($"UPDATE DatabaseVersion SET Version = {newVersion}");
    }
}