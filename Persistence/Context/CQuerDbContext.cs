using Common.DataModels.IdentityManagement;
using Common.DataModels.StandardEntities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Persistence.Context
{
    class CQuerDbContext : DbContext, ICQuerDbContext
    {
        public CQuerDbContext(DbContextOptions<CQuerDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<FileReference> FileReferences { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
