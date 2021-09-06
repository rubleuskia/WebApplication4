using System.Threading.Tasks;
using AngleSharp.Dom;
using WebApplication.Tests.Integration.Infrastructure;
using Xunit;

namespace WebApplication.Tests.Integration.Accounts
{
    public class AccountsTests : IClassFixture<WebAppWebApplicationFactory>
    {
        private readonly AccountsFacade _facade;

        public AccountsTests(WebAppWebApplicationFactory factory)
        {
            _facade = new AccountsFacade(factory);
        }

        [Fact]
        public async Task Accounts_Index_ReturnsAccountsListView()
        {
            // arrange
            var accountId1 = await _facade.CreateAccount(100500, "USD");
            var accountId2 = await _facade.CreateAccount(500100, "EUR");

            // act
            var document = await _facade.GetMvcAccounts();

            // assert
            IHtmlCollection<IElement> cards = document.QuerySelectorAll(".card");
            HtmlHelper.AssertAccountCard(cards, accountId1, "US dollar (USD)", "100500,000");
            HtmlHelper.AssertAccountCard(cards, accountId2, "Euro (EUR)", "500100,000");
        }
    }
}