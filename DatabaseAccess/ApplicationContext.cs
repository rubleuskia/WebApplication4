using DatabaseAccess.Entities;
using DatabaseAccess.Quizzes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Account> Accounts { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
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

            builder.Entity<Account>()
                .Property(a => a.Version)
                .IsRowVersion();

            builder.Entity<Quiz>().HasMany(x => x.Questions)
                .WithOne(x => x.Quiz)
                .HasForeignKey(x => x.QuizId);

            builder.Entity<QuizAnswerHistoryRecord>().HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId);

            builder.Entity<QuizRatingRecord>().HasKey(r => new {r.QuizId, r.UserId});
            builder.Entity<QuizRatingRecord>().HasOne(x => x.Quiz)
                .WithMany()
                .HasForeignKey(x => x.QuizId);

            builder.Entity<QuizRatingRecord>().HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.Entity<QuizCompletionRecord>().HasKey(r => new {r.QuizId, r.UserId});

            builder.Entity<QuizCompletionRecord>().HasOne(x => x.Quiz)
                .WithMany()
                .HasForeignKey(x => x.QuizId);

            builder.Entity<QuizCompletionRecord>().HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);
        }

        private static void CreateSeedUsers(ModelBuilder builder)
        {
            var role = new IdentityRole
            {
                Id = "266fa2f3-766c-42b4-a409-9a7abd6e0b84",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "75645469-96e7-4aff-9119-019d84c16984",
            };

            var user = new User
            {
                Id = "08b335ba-bd9c-4ed0-8fb0-23c4551f272d",
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                ConcurrencyStamp = "791eb30f-3fa2-4116-bc66-b67473be8e4c",
                SecurityStamp = "e0fef372-3022-4681-8889-95571a6dddf3",
                Age = 100,
                // PasswordHash = new PasswordHasher<User>().HashPassword(null, _seedOptions.Value.AdminUserPassword)
                PasswordHash = "AQAAAAEAACcQAAAAEC27dnWY85PP6ENmRFIZpc04i3OusxPvrs/B9Jybhmt6hHy3KojwGcWq5D1KhCFutg=="
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