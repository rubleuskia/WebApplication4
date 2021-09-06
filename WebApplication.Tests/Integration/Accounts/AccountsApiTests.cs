using System.Threading.Tasks;
using FluentAssertions;
using WebApplication.Tests.Integration.Infrastructure;
using Xunit;

namespace WebApplication.Tests.Integration.Accounts
{
    public class AccountsApiTests : IClassFixture<WebAppWebApplicationFactory>
    {
        private readonly AccountsFacade _facade;

        public AccountsApiTests(WebAppWebApplicationFactory factory)
        {
            _facade = new AccountsFacade(factory);
        }

        [Fact]
        public async Task GetAccounts_AccountCreated_ReturnsExpectedAccount()
        {
            // arrange
            var accountId = await _facade.CreateAccount(100500, "USD");

            // act
            var accounts = await _facade.GetAccounts();

            // assert
            var account = accounts.Should().ContainSingle(x => x.Id == accountId).Which;
            account.Amount.Should().Be(100500);
            account.CurrencyName.Should().Be("US dollar (USD)");
        }
    }
}