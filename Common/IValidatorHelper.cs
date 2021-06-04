namespace Common
{
    public interface IValidatorHelper
    {
        public bool IsLocalAddress(string localApiUrl);
        public bool CanEstablishConnectionWithDatabase(string connectionString);
        public bool HaveRequiredPermissionsToFileStore(string fileStorePath);
    }
}
