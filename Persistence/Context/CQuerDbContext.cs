using Common.DataModels.IdentityManagement;
using Common.DataModels.StandardEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public class CQuerDbContext : DbContext
    {
        public CQuerDbContext(DbContextOptions<CQuerDbContext> options) : base(options) { }

        //TODO: Add models & DbSets
        public DbSet<Account> Accounts { get; set; }
        public DbSet<FileReference> FileReferences { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
