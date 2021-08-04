using System.Threading;
using System.Threading.Tasks;
using Core.TelegramBot;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using WebApplication4.Options;

namespace WebApplication4.HostedServices
{
    public class TelegramHostedService : IHostedService
    {
        private readonly ITelegramBotService _telegramBotService;
        private readonly string _apiKey;

        public TelegramHostedService(ITelegramBotService telegramBotService, IOptions<TelegramBotOptions> options)
        {
            _telegramBotService = telegramBotService;
            _apiKey = options.Value.ApiKey;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return _telegramBotService.StartAsync(_apiKey);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return _telegramBotService.StopAsync();
        }
    }
}
