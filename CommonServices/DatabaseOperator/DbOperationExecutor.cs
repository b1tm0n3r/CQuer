using Common.DataModels.StandardEntities;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonServices.DatabaseOperator
{
    abstract class DbOperationExecutor
    {
        private CQuerDbContext dbContext;
        protected DbOperationExecutor(CQuerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        //Add actions
        protected void AddFileReference(FileReference fileReference)
        {
            dbContext.FileReferences.Add(fileReference);
            dbContext.SaveChanges();
        }
        //Get actions
        protected string GetFilePath(string fileName)
        {
            return dbContext.FileReferences.FirstOrDefault(x => x.FileName==fileName).Path;
        }
        protected string GetFileChecksum(string fileName)
        {
            return dbContext.FileReferences.FirstOrDefault(x => x.FileName == fileName).SHA256Hash;
        }


    }
}
