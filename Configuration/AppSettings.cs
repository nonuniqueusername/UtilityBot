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
                Console.WriteLine("there is must be env variable BotToken");
                Environment.Exit(1);
                //throw new Exception("there is must be env variable BotToken");
            }
            BotToken = botToken;
        }
    }
}
