using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApp.Tests.Integration.Infrastructure
{
    public static class HttpClientExtensions
    {
        public static async Task Authenticate(this HttpClient client)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Email", "admin@admin.com"),
                new KeyValuePair<string, string>("Password", "User123!"),
            });

            var response = await client.PostAsync("/UserAccount/Login", formContent);
            response.EnsureSuccessStatusCode();
        }
    }
}