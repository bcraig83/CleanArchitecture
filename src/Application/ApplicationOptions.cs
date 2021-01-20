namespace Application
{
    public class ApplicationOptions
    {
        public const string AppSettingsFileLocation = "Application";
        public bool StoreAuthorInLowercase { get; set; } = false;

        public override string ToString()
        {
            return $"{{{nameof(StoreAuthorInLowercase)}={StoreAuthorInLowercase}}}";
        }
    }
}