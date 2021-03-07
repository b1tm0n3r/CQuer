using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using Common.DataModels.StandardEntities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace CommonServicesTests
{
    public class DbContextStub : ICQuerDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FileReference> FileReferences { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }
        public DbContextStub(DbSet<Account> accounts)
        {
            Accounts = accounts;
        }
    }
}