using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using TelegramBotApp;
using WebApplication4.Configuration;

namespace WebApplication4.TelegramBot
{
    public class TelegramBotHostedService : IHostedService
    {
        private readonly string _apiKey;
        private readonly ITelegramAppService _telegramAppService;

        public TelegramBotHostedService(IOptions<TelegramBotOptions> options, ITelegramAppService telegramAppService)
        {
            _telegramAppService = telegramAppService;
            _apiKey = options.Value.ApiKey;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _telegramAppService.Start(_apiKey);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _telegramAppService.Dispose();
            return Task.CompletedTask;
        }
    }
}
