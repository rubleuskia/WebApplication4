using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Core.Accounting.Dtos;
using FluentAssertions;
using WebApplication.Tests.Integration.Infrastructure;
using Xunit;

namespace WebApplication.Tests.Integration.Accounts
{
    public class AccountsApiTests : IClassFixture<WebAppWebApplicationFactory>
    {
        private readonly WebAppWebApplicationFactory _factory;

        public AccountsApiTests(WebAppWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAccounts_EmptyDatabase_ReturnsEmptyAccountsList()
        {
            // arrange
            HttpClient client = _factory.CreateClient();
            await client.Authenticate();

            // act
            HttpResponseMessage response = await client.GetAsync("api/accountsApi");

            // assert
            response.EnsureSuccessStatusCode();
            var accounts = await response.Content.ReadFromJsonAsync<AccountDto[]>();
            accounts.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAccounts_AccountCreated_ReturnsExpectedAccount()
        {
            // arrange
            HttpClient client = _factory.CreateClient();
            await client.Authenticate();
            var accountId = await CreateAccount(client, 100500, "USD");

            // act
            HttpResponseMessage response = await client.GetAsync("api/accountsApi");

            // assert
            response.EnsureSuccessStatusCode();
            var accounts = await response.Content.ReadFromJsonAsync<AccountDto[]>();
            var account = accounts.Should().ContainSingle(x => x.Id == accountId).Which;
            account.Amount.Should().Be(100500);
            account.CurrencyName.Should().Be("US dollar (USD)");
        }

        private async Task<Guid> CreateAccount(HttpClient client, decimal amount, string currency)
        {
            var data = new CreateAccountDto
            {
                Amount = amount,
                Currency = currency,
            };

            var response = await client.PostAsJsonAsync("api/accountsApi/create", data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Guid>();
        }
    }
}