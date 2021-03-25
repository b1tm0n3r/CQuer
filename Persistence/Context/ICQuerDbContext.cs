using Common.DataModels.IdentityManagement;
using Common.DataModels.StandardEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public interface ICQuerDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<Account> Accounts { get; set; }
        DbSet<FileReference> FileReferences { get; set; }
        DbSet<Ticket> Tickets { get; set; }

        Task<int> SaveChangesAsync();
    }
}
