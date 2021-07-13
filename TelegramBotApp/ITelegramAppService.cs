using System;
using System.Threading.Tasks;

namespace TelegramBotApp
{
    public interface ITelegramAppService : IDisposable
    {
        Task Start(string apiKey);
    }
}
