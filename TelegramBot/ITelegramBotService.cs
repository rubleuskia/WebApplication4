using System.Threading.Tasks;

namespace TelegramBot
{
    public interface ITelegramBotService
    {
        Task StartAsync(string apiKey);
        Task StopAsync();
    }
}