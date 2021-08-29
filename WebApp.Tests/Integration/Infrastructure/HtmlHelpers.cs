using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace WebApp.Tests.Integration.Infrastructure
{
    public class HtmlHelper
    {
        public static async Task<IDocument> GetHtmlDocument(HttpResponseMessage response)
        {
            var config = Configuration.Default;
            var context = BrowsingContext.New(config);
            var html = await response.Content.ReadAsStringAsync();
            return await context.OpenAsync(req => req.Content(html));
        }
    }
}