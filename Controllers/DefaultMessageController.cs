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
    internal class DefaultMessageController : MessageController
    {
        public DefaultMessageController(ITelegramBotClient tgClient, IState state) : base(tgClient, state) { }

        public override async Task Handle(Message message, CancellationToken ct)
        {
            await base.Handle(message, ct);
        }
    }
}
