using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using UtilityBot.Services;

namespace UtilityBot.Controllers
{
    public abstract class MessageController
    {
        protected readonly ITelegramBotClient TelegramClient;
        protected readonly IState _state;

        public MessageController(ITelegramBotClient tgClient, IState state)
        {
            TelegramClient = tgClient;
            _state = state;
        }
        virtual public async Task Handle(Message message, CancellationToken ct)
        {
            Console.WriteLine($"{GetType().Name} controller recieved message");
        }
        virtual public async Task Handle(CallbackQuery? message, CancellationToken ct)
        {
            Console.WriteLine($"{GetType().Name} controller recieved message");
        }
    }
}
