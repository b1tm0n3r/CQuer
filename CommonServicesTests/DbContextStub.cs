using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using Common.DataModels.StandardEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Persistence.Context;

namespace CommonServicesTests
{
    public class DbContextStub : ICQuerDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FileReference> FileReferences { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DatabaseFacade Database { get; }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(1);
        }
        public DbContextStub()
        {
            
        }
        public DbContextStub(DbSet<Account> accounts)
        {
            Accounts = accounts;
        }
        public DbContextStub(DbSet<Ticket> tickets)
        {
            Tickets = tickets;
        }
        public DbContextStub(DbSet<Account> accounts, DbSet<Ticket> tickets)
        {
            Accounts = accounts;
            Tickets = tickets;
        }

    }
}