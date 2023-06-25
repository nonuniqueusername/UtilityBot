using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using UtilityBot.Models;
using UtilityBot.Services;
using UtilityBot.Utilities;

namespace UtilityBot.Controllers
{
    internal class TextMessageController : MessageController
    {
        public TextMessageController(ITelegramBotClient tgClient, IState state) : base(tgClient, state) { }

        public override async Task Handle(Message message, CancellationToken ct)
        {
            await base.Handle(message, ct);
            switch (message.Text)
            {
                case "/start":
                    ShowInlineKbd(message, ct);
                    break;
                default:
                    switch (_state.GetStateCurrentMode(message.Chat.Id))
                    {
                        case StateMode.CountSum:
                            int sum = Utilities.Math.Sum(message.Text);
                            await TelegramClient.SendTextMessageAsync(
                                message.Chat.Id,
                                $"Сумма чисел: {sum.ToString()}",
                                cancellationToken: ct
                            );
                            _state.SetStateCurrentMode(StateMode.None, message.Chat.Id);
                            await ShowInlineKbd(message, ct);

                            break;
                        case StateMode.CountWords:
                            int wordCount = WordCounter.Count(message.Text);
                            await TelegramClient.SendTextMessageAsync(
                                message.Chat.Id,
                                $"В вашем сообщении {wordCount.ToString()} символов",
                                cancellationToken: ct
                            );
                            _state.SetStateCurrentMode(StateMode.None, message.Chat.Id);
                            await ShowInlineKbd(message, ct);
                            break;
                        default:
                            await ShowInlineKbd(message, ct);
                            break;
                    }
                    break;
            }
        }
        private async Task ShowInlineKbd(Message message, CancellationToken ct)
        {
            List<InlineKeyboardButton[]> buttons = new List<InlineKeyboardButton[]>();
            buttons.Add(new[]
            {
                        InlineKeyboardButton.WithCallbackData("Count words","count_words"),
                        InlineKeyboardButton.WithCallbackData("Do math","math")

                    });
            await TelegramClient.SendTextMessageAsync(
                message.Chat.Id,
                "Wut to do?",
                cancellationToken: ct,
                parseMode: ParseMode.Html,
                replyMarkup: new InlineKeyboardMarkup(buttons)
                );
        }
    }
}
