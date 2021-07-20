using DatabaseAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}