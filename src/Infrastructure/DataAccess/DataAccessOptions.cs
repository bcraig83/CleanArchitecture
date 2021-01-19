namespace DataAccess
{
    public class DataAccessOptions
    {
        public const string AppSettingsFileLocation = "Infrastructure:DataAccess";
        public string PersistenceMechanism { get; set; } = "InMemory"; // One of InMemory or EntityFramework.
        public bool UseEntityFrameworkInMemoryDatabase { get; set; } = true; // If using EF, we can still use an in-memory database.
    }
}