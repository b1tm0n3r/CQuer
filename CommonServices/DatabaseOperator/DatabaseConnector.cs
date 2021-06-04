using Persistence.Context;
using System;

namespace CommonServices.DatabaseOperator
{
    class DatabaseConnector : DbOperationExecutor, IDatabaseConnector
    {
        public DatabaseConnector(ICQuerDbContext dbContext) : base(dbContext)
        {
        }

        public void DownloadFile(string fileSource)
        {
            throw new NotImplementedException();
        }

        //TODO: Possibly change to Tasks in the future
        public string GetLocalFilePath(string fileName)
        {
            return GetFilePath(fileName);
        }
    }
}
