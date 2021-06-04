using Common.DataModels.StandardEntities;
using Persistence.Context;
using System.Linq;
using System.Threading.Tasks;

namespace CommonServices.DatabaseOperator
{
    abstract class DbOperationExecutor
    {
        private readonly ICQuerDbContext dbContext;
        protected DbOperationExecutor(ICQuerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Add actions
        protected async Task AddFileReference(FileReference fileReference)
        {
            dbContext.FileReferences.Add(fileReference);
            await dbContext.SaveChangesAsync();
        }
        //Get actions
        protected string GetFilePath(string fileName)
        {
            return dbContext.FileReferences.FirstOrDefault(x => x.FileName==fileName).Path;
        }
        protected string GetFileChecksum(string fileName)
        {
            return dbContext.FileReferences.FirstOrDefault(x => x.FileName == fileName).Sha256Checksum;
        }
    }
}
