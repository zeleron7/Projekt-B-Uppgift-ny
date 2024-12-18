namespace Configuration;

public class DbConnectionSetsOptions
{
    public const string Position = "ConnectionSets";

    public List<DbSetDetailOptions> DataSets {get; set;}
    public List<DbSetDetailOptions> IdentitySets {get; set;}
}

public class DbSetDetailOptions
{
    public string DbTag { get; set; }
    public string DbServer { get; set; }

    public List<DbConnectionDetail> DbConnections { get; set; }
}
