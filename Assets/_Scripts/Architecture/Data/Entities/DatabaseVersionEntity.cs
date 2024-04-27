using SQLite4Unity3d;

public class DatabaseVersionEntity
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public int version { get; set; }
}