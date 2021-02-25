using Microsoft.EntityFrameworkCore;
using Persistence.DataModels;
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
        DbSet<Account> Accounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}
