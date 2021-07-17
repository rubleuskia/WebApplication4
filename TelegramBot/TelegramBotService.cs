using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Currencies.Common.Infos;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public class TelegramBotService : ITelegramBotService
    {
        private readonly ICurrencyInfoService _currencyInfoService;
        private CancellationTokenSource _cancellationTokenSource;
        private TelegramBotClient _botClient;

        public TelegramBotService(ICurrencyInfoService currencyInfoService)
        {
            _currencyInfoService = currencyInfoService;
        }

        public Task StartAsync(string apiKey)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                return Task.CompletedTask;
            }

            _botClient = new TelegramBotClient(apiKey);

            _cancellationTokenSource = new CancellationTokenSource();

            _botClient.StartReceiving(
                new DefaultUpdateHandler(HandleUpdateAsync, HandleErrorAsync),
                _cancellationTokenSource.Token
            );

           return Task.CompletedTask;
        }

        public Task StopAsync()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            return Task.CompletedTask;
        }

        private  async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken ct)
        {
            var handler = update.Type switch
            {
                UpdateType.Message => BotOnMessageReceived(update.Message),
                UpdateType.EditedMessage => BotOnMessageReceived(update.Message),
                UpdateType.CallbackQuery => BotOnCallbackQueryReceived(update.CallbackQuery),
                UpdateType.InlineQuery => BotOnInlineQueryReceived(update.InlineQuery),
                UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(update.ChosenInlineResult),
                _ => UnknownUpdateHandlerAsync(update)
            };

            try
            {
                await handler;
            }
            catch (Exception exception)
            {
                await HandleErrorAsync(botClient, exception, ct);
            }
        }

        private async Task BotOnMessageReceived(Message message)
        {
            Console.WriteLine($"Receive message type: {message?.Type}");
            if (message == null || message.Type != MessageType.Text)
            {
                return;
            }

            var action = (message.Text.Split(' ').First()) switch
            {
                "/inline" => SendInlineKeyboard(message),
                "/keyboard" => SendReplyKeyboard(message),
                "/photo" => SendFile(message),
                "/request" => RequestContactAndLocation(message),
                "/rate" => RequestRate(message),
                _ => Usage(message)
            };

            await action;

            // Send inline keyboard
            // You can process responses in BotOnCallbackQueryReceived handler
             async Task SendInlineKeyboard(Message message)
            {
                await _botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

                // Simulate longer running task
                await Task.Delay(500);

                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    // first row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("1.1", "11"),
                        InlineKeyboardButton.WithCallbackData("1.2", "12"),
                    },
                    // second row
                    new []
                    {
                        InlineKeyboardButton.WithCallbackData("2.1", "21"),
                        InlineKeyboardButton.WithCallbackData("2.2", "22"),
                    }
                });

                await _botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Choose",
                    replyMarkup: inlineKeyboard
                );
            }

             async Task SendReplyKeyboard(Message message)
            {
                var replyKeyboardMarkup = new ReplyKeyboardMarkup(
                    new KeyboardButton[][]
                    {
                        new KeyboardButton[] { "1.1", "1.2" },
                        new KeyboardButton[] { "2.1", "2.2" },
                    },
                    resizeKeyboard: true
                );

                await _botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Choose",
                    replyMarkup: replyKeyboardMarkup
                );
            }

             async Task SendFile(Message message)
            {
                await _botClient.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

                const string filePath = @"Files/tux.png";

                using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

                var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

                await _botClient.SendPhotoAsync(
                    chatId: message.Chat.Id,
                    photo: new InputOnlineFile(fileStream, fileName),
                    caption: "Nice Picture"
                );
            }

            async Task RequestContactAndLocation(Message message)
            {
                var requestReplyKeyboard = new ReplyKeyboardMarkup(new[]
                {
                    KeyboardButton.WithRequestLocation("Location"),
                    KeyboardButton.WithRequestContact("Contact"),
                });

                await _botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: "Who or Where are you?",
                    replyMarkup: requestReplyKeyboard
                );
            }

            async Task Usage(Message message)
            {
                const string usage = "Usage:\n" +
                                        "/inline   - send inline keyboard\n" +
                                        "/keyboard - send custom keyboard\n" +
                                        "/photo    - send a photo\n" +
                                        "/request  - request location or contact";

                await _botClient.SendTextMessageAsync(
                    chatId: message.Chat.Id,
                    text: usage,
                    replyMarkup: new ReplyKeyboardRemove()
                );
            }
        }

        private async Task RequestRate(Message message)
        {
            var charCode = message.Text.Split(" ").Last();
            var rate = await _currencyInfoService.GetCurrencyRate(charCode);

            await _botClient.SendTextMessageAsync(
                chatId: message.Chat.Id,
                text: $"{charCode} rate today is: {rate}"
            );
        }

        // Process Inline Keyboard callback data
        private async Task BotOnCallbackQueryReceived(CallbackQuery callbackQuery)
        {
            await _botClient.AnswerCallbackQueryAsync(
                callbackQuery.Id,
                $"Received {callbackQuery.Data}"
            );

            await _botClient.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                $"Received {callbackQuery.Data}"
            );
        }

        #region Inline Mode

        private async Task BotOnInlineQueryReceived(InlineQuery inlineQuery)
        {
            Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

            InlineQueryResultBase[] results = {
                // displayed result
                new InlineQueryResultArticle(
                    id: "3",
                    title: "TgBots",
                    inputMessageContent: new InputTextMessageContent(
                        "hello"
                    )
                )
            };

            await _botClient.AnswerInlineQueryAsync(inlineQuery.Id, results, isPersonal: true, cacheTime: 0);
        }

        private static Task BotOnChosenInlineResultReceived(ChosenInlineResult chosenInlineResult)
        {
            Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
            return Task.CompletedTask;
        }

        #endregion

        private static Task UnknownUpdateHandlerAsync(Update update)
        {
            Console.WriteLine($"Unknown update type: {update.Type}");
            return Task.CompletedTask;
        }

        private static Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            return Task.CompletedTask;
        }

    }
}
