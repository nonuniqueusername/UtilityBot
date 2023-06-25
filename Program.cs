using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Telegram.Bot;
using UtilityBot;
using UtilityBot.Configuration;
using UtilityBot.Controllers;
using UtilityBot.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;

        IHost host = new HostBuilder()
            .ConfigureServices(services => ConfigureServices(services))
            .UseConsoleLifetime()
            .Build();
        Console.WriteLine("Service started");
        await host.RunAsync();
        Console.WriteLine("Service stopped");
    }

    static void ConfigureServices(IServiceCollection services)
    {
        AppSettings appSettings = BuildAppSettings();
        services.AddSingleton(appSettings);
        services.AddSingleton<IState, StateMachine>();

        services.AddTransient<InlineKbdController>();
        services.AddTransient<TextMessageController>();
        services.AddTransient<DefaultMessageController>();

        services.AddSingleton<ITelegramBotClient>(p => new TelegramBotClient(appSettings.BotToken));
        services.AddHostedService<Bot>();
    }

    static AppSettings BuildAppSettings()
    {
        return new AppSettings();
    }
}