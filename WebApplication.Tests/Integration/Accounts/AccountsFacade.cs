using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Core.Accounting.Dtos;
using WebApplication.Tests.Integration.Infrastructure;

namespace WebApplication.Tests.Integration.Accounts
{
    public class AccountsFacade
    {
        private readonly HttpClient _client;
        private const string ApiRoute = "api/accountsApi";

        public AccountsFacade(WebAppWebApplicationFactory factory)
        {
            _client = factory.CreateClient();
        }

        public async Task<AccountDto[]> GetAccounts()
        {
            await _client.Authenticate();

            var response = await _client.GetAsync("api/accountsApi");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AccountDto[]>();
        }

        public async Task<IDocument> GetMvcAccounts()
        {
            await _client.Authenticate();

            var response = await _client.GetAsync("/accounts/index");
            response.EnsureSuccessStatusCode();
            return await HtmlHelper.GetHtmlDocument(response);
        }

        public async Task<Guid> CreateAccount(decimal amount, string currency)
        {
            await _client.Authenticate();

            var data = new CreateAccountDto
            {
                Amount = amount,
                Currency = currency,
            };

            var response = await _client.PostAsJsonAsync($"{ApiRoute}/create", data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }
    }
}