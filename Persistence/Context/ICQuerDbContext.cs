using Common.DataModels.IdentityManagement;
using Common.DataModels.StandardEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context
{
    public interface ICQuerDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<FileReference> FileReferences { get; set; }
        DbSet<Ticket> Tickets { get; set; }

        Task<int> SaveChangesAsync();
    }
}
