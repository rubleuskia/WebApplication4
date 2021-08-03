using System.Threading.Tasks;

namespace Core.TelegramBot
{
    public interface ITelegramBotService
    {
        Task StartAsync(string apiKey);
        Task StopAsync();
    }
}