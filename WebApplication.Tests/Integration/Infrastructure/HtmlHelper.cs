using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using FluentAssertions;

namespace WebApplication.Tests.Integration.Infrastructure
{
    public class HtmlHelper
    {
        public static async Task<IDocument> GetHtmlDocument(HttpResponseMessage response)
        {
            var html = await response.Content.ReadAsStringAsync();
            return await BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(html));
        }

        public static void AssertAccountCard(IHtmlCollection<IElement> cards, Guid accountId, string currency, string amount)
        {
            var card = cards.Single(x => x.QuerySelector(".card-header").TextContent.Contains(accountId.ToString()));
            card.QuerySelector(".card-text").TextContent.Should().Be(currency);
            card.QuerySelector(".card-title").TextContent.Should().Be(amount);
        }
    }
}