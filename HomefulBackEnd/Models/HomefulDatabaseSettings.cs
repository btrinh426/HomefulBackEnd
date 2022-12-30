namespace HomefulBackEnd.Models
{
    public class HomefulDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ProfilesCollectionName { get; set; } = null!;
    }
}
