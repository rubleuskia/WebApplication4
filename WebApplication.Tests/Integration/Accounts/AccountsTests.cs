using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp.Dom;
using FluentAssertions;
using WebApplication.Tests.Integration.Infrastructure;
using Xunit;

namespace WebApplication.Tests.Integration.Accounts
{
    public class AccountsTests : IClassFixture<WebAppWebApplicationFactory>
    {
        private readonly WebAppWebApplicationFactory _factory;

        public AccountsTests(WebAppWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Accounts_Index_ReturnsAccountsListView()
        {
            // arrange
            HttpClient client = _factory.CreateClient();
            await client.Authenticate();

            // act
            HttpResponseMessage response = await client.GetAsync("/accounts/index");

            // assert
            response.EnsureSuccessStatusCode();
            IDocument document = await HtmlHelper.GetHtmlDocument(response);
            IHtmlCollection<IElement> cards = document.QuerySelectorAll(".card");
            cards.Should().BeEmpty();
        }
    }
}