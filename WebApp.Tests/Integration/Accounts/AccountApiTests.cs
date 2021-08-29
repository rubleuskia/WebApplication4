using System.Threading.Tasks;
using Core.Accounting.Dtos;
using FluentAssertions;
using WebApp.Tests.Integration.Infrastructure;
using Xunit;

namespace WebApp.Tests.Integration.Accounts
{
    public class AccountApiTests : IClassFixture<WebAppWebApplicationFactory>
    {
        private readonly AccountsApiFacade _api;

        public AccountApiTests(WebAppWebApplicationFactory factory)
        {
            _api = new AccountsApiFacade(factory.CreateClient());
        }

        [Fact]
        public async Task CreateAccount_Always_ReturnsNewAccount()
        {
            // arrange & act
            var accountId =  await _api.CreateAccount(100, "USD");

            // assert
            var accounts = await _api.GetAccounts();
            accounts.Should().ContainSingle(x => x.Id == accountId).Which
                .Should().BeEquivalentTo(new AccountDto
                {
                    Amount = 100,
                    CurrencyName = "US dollar (USD)",
                    Id = accountId,
                });
        }
    }
}