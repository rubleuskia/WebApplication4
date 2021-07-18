using DataAccess.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public sealed class ApplicationContext : IdentityDbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
    }
}