using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Exceptions;
using UtilityBot.Controllers;

namespace UtilityBot
{
    internal class Bot : BackgroundService
    {
        private readonly ITelegramBotClient _tgClient;
        private readonly MessageController _inlineKbdController;
        private readonly MessageController _textMessageController;
        private readonly MessageController _defaultMessageController;

        public Bot(
            ITelegramBotClient tgClient,
            InlineKbdController inlineKbdController,
            TextMessageController textMessageController,
            DefaultMessageController defaultMessageController
            )
        {
            _tgClient = tgClient;
            _inlineKbdController = inlineKbdController;
            _textMessageController = textMessageController;
            _defaultMessageController = defaultMessageController;
        }

        protected override async Task ExecuteAsync(CancellationToken ct)
        {
            _tgClient.StartReceiving
                (
                UpdateHandler,
                ErrorHandler,
                new Telegram.Bot.Polling.ReceiverOptions()
                {
                    AllowedUpdates = { }
                },
                cancellationToken: ct
                );
            Console.WriteLine("Bot started");
        }

        async Task UpdateHandler(ITelegramBotClient tgClient, Update update, CancellationToken ct)
        {
            if (update.Type == UpdateType.CallbackQuery)
            {
                await _inlineKbdController.Handle(update.CallbackQuery, ct);
                return;
            }
            if (update.Type == UpdateType.Message)
            {
                switch (update.Message!.Type)
                {
                    case MessageType.Text:
                        await _textMessageController.Handle(update.Message, ct);
                        return;
                    default:
                        await _defaultMessageController.Handle(update.Message, ct);
                        return;
                }
            }
        }

        Task ErrorHandler(ITelegramBotClient tgClient, Exception ex, CancellationToken ct)
        {
            string errMessage = ex switch

            {
                ApiRequestException arx => $"Telegram API error:\v{arx.ErrorCode}\n{arx.Message}",
                _ => ex.Message
            };
            Console.WriteLine(errMessage);
            Console.WriteLine("w8 10s will reconect");
            Thread.Sleep(10000);
            return Task.CompletedTask;
        }
    }
}
