using System;
using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DatabaseAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        private readonly IOptions<SeedDataOptions> _seedOptions;
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(IOptions<SeedDataOptions> seedOptions, DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            _seedOptions = seedOptions;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            CreateSeedUsers(builder);

            builder.Entity<IdentityRole>()
                .HasMany<IdentityUserRole<string>>()
                .WithOne()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Account>()
                .HasOne(x => x.User)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.UserId);
        }

        private void CreateSeedUsers(ModelBuilder builder)
        {
            var role = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            };

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Age = 100,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, _seedOptions.Value.AdminUserPassword)
            };

            builder.Entity<IdentityRole>().HasData(role);
            builder.Entity<User>().HasData(user);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            });
        }
    }
}