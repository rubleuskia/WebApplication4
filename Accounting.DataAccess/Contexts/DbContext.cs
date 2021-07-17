using Accounting.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accounting.DataAccess.Contexts
{
    public sealed class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}