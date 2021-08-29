using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Core.Accounting.Dtos;
using WebApp.Tests.Integration.Infrastructure;

namespace WebApp.Tests.Integration.Accounts
{
    public class AccountsApiFacade
    {
        private const string AccountApiRoute = "api/AccountsApi";
        private readonly HttpClient _client;

        public AccountsApiFacade(HttpClient client)
        {
            _client = client;
        }

        public async Task<AccountDto[]> GetAccounts()
        {
            await _client.Authenticate();

            var response = await _client.GetAsync(AccountApiRoute);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<AccountDto[]>();
        }

        public async Task<Guid> CreateAccount(decimal amount, string currency)
        {
            await _client.Authenticate();

            var data = new CreateAccountDto
            {
                Amount = amount,
                Currency = currency,
            };

            var response = await _client.PostAsJsonAsync($"{AccountApiRoute}/create", data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }
    }
}