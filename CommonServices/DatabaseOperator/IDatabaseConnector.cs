namespace CommonServices.DatabaseOperator
{
    public interface IDatabaseConnector
    {
        public void DownloadFile(string fileSource);
        public string GetLocalFilePath(string fileName);
    }
}
