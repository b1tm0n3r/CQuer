using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonServices.DatabaseOperator
{
    class DatabaseConnector : DbOperationExecutor
    {
        public DatabaseConnector(CQuerDbContext dbContext) : base(dbContext)
        {
            dbContext.Database.EnsureCreated();
        }

        //TODO: Possibly change to Tasks in the future
        public string GetLocalFilePath(string fileName)
        {
            return GetFilePath(fileName);
        }


    }
}
