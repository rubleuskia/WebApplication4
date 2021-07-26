using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DatabaseAccess
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Quiz> Quizzes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Account>()
                .HasOne(x => x.User)
                .WithMany(x => x.Accounts)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Quiz>()
                .HasMany(x => x.Questions)
                .WithOne(x => x.Quiz)
                .HasForeignKey(x => x.QuizId);

            builder.Entity<Quiz>()
                .HasMany(x => x.QuizCompletions)
                .WithOne(x => x.Quiz)
                .HasForeignKey(x => x.QuizId);

            builder.Entity<QuizQuestionUserAnswer>()
                .HasOne(x => x.QuizQuestion)
                .WithMany()
                .HasForeignKey(x => x.QuizQuestionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<QuizQuestionUserAnswer>()
                .HasOne(x => x.QuizCompletionHistory)
                .WithMany(x => x.UserAnswers)
                .HasForeignKey(x => x.QuizCompletionHistoryId);

            builder.Entity<QuizQuestionUserAnswer>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.Entity<QuizRating>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.Entity<QuizRating>()
                .HasOne(x => x.Quiz)
                .WithMany()
                .HasForeignKey(x => x.QuizId);

            builder.Entity<QuizCompletionHistory>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            CreateSeededUser(builder);
        }

        private static void CreateSeededUser(ModelBuilder builder)
        {
            var role = new IdentityRole
            {
                Id = "5b9ef978-d85b-4050-b636-c0f4b4f4f708",
                Name = "admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "9906c2f4-4941-4f1e-ae6e-6b67258c526f",
            };

            var user = new User
            {
                Id = "bdb94046-f01d-4080-a989-341c3e88ed50",
                Email = "admin@admin.com",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Age = 100,
                PasswordHash = "AQAAAAEAACcQAAAAEOlRAst5kneREHoKVJsYzh3MhWbA3Z9lJl2aw/Hk4DO9C9gtXT/CiI8Q7ND1arCpQA==",
                ConcurrencyStamp = "c568026f-8944-41b6-8fcc-84c613158e27",
                SecurityStamp = "7b8588f2-6429-4d36-9097-dcc81abdf4a7",
            };

            builder.Entity<User>().HasData(user);
            builder.Entity<IdentityRole>().HasData(role);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "5b9ef978-d85b-4050-b636-c0f4b4f4f708",
                UserId = "bdb94046-f01d-4080-a989-341c3e88ed50",
            });
        }
    }
}