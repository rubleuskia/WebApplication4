using System.Net.Http;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;

namespace WebApplication.Tests.Integration.Infrastructure
{
    public class HtmlHelper
    {
        public static async Task<IDocument> GetHtmlDocument(HttpResponseMessage response)
        {
            var html = await response.Content.ReadAsStringAsync();
            return await BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(html));
        }
    }
}