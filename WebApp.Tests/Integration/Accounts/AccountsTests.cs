using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using WebApp.Tests.Integration.Infrastructure;
using Xunit;

namespace WebApp.Tests.Integration.Accounts
{
    public class AccountsTests : IClassFixture<WebAppWebApplicationFactory>
    {
        private readonly WebAppWebApplicationFactory _factory;
        private readonly AccountsApiFacade _api;

        public AccountsTests(WebAppWebApplicationFactory factory)
        {
            _factory = factory;
            _api = new AccountsApiFacade(factory.CreateClient());
        }

        [Fact]
        public async Task Accounts_Index_ReturnsAccountsPage()
        {
            // arrange
            var client = _factory.CreateClient();
            await client.Authenticate();
            var accountId = await _api.CreateAccount(100, "USD");

            // act
            var response = await client.GetAsync("/accounts/index");

            // assert
            response.EnsureSuccessStatusCode();
            var cards = (await HtmlHelper.GetHtmlDocument(response)).QuerySelectorAll(".card");
            var card = cards.Single(card => card.QuerySelector(".card-header").TextContent.Contains(accountId.ToString()));
            card.QuerySelector(".card-title").TextContent.Should().BeEquivalentTo("100,000");
            card.QuerySelector(".card-text").TextContent.Should().BeEquivalentTo("US dollar (USD)");
        }
    }
}