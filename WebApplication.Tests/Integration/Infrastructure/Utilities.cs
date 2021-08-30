using DatabaseAccess;

namespace WebApplication.Tests.Integration.Infrastructure
{
    public class Utilities
    {
        public static void InitializeDbForTests(ApplicationContext context)
        {
            // context.Accounts.Add(new Account
            // {
            //     Amount = 100500,
            //     Id = Guid.NewGuid(),
            // });
        }
    }
}