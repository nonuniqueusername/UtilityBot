using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBot.Models;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    internal class InlineKbdController : MessageController
    {
        public InlineKbdController(ITelegramBotClient tgClient, IState state) : base(tgClient, state) { }

        public override async Task Handle(CallbackQuery? message, CancellationToken ct)
        {
            await base.Handle(message, ct);

            if (message?.Data == null) return;

            switch (message?.Data)
            {
                case "count_words":
                    _state.SetStateCurrentMode(StateMode.CountWords, message.From.Id);
                    TelegramClient.SendTextMessageAsync(
                        message.From.Id,
                        "Send message to count words",
                        cancellationToken: ct
                        );
                    break;
                case "math":
                    _state.SetStateCurrentMode(StateMode.CountSum, message.From.Id);
                    TelegramClient.SendTextMessageAsync(
                        message.From.Id,
                        "Send integers to sum em",
                        cancellationToken: ct
                        );
                    break;
                default:
                    _state.SetStateCurrentMode(StateMode.None, message.From.Id);
                    throw new Exception($"Non implemented case {message?.Data}");
                    break;
            }
        }
    }
}
