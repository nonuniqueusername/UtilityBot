using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Configuration
{
    internal class AppSettings
    {
        public string BotToken { get; set; }

        public AppSettings() 
        {
            string botToken = Environment.GetEnvironmentVariable("BotToken");
            if (string.IsNullOrEmpty(botToken))
            {
                throw new Exception("there is must be env variable BotToken");
            }
            BotToken = botToken;
        }
    }
}
