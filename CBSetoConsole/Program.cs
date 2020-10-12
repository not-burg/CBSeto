using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using CBSetoConsole.Services;
using CBSetoLib.Services;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

using Microsoft.Extensions.DependencyInjection;

namespace CBSetoConsole
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await using var services = ConfigureServices();

            var client = services.GetRequiredService<DiscordSocketClient>();

            client.Log += LogAsync;
            services.GetRequiredService<CommandService>().Log += LogAsync;

            var token = Environment.GetEnvironmentVariable("DiscordBotToken");
            if (token is null) throw new Exception("DiscordBotToken not found on current machine.");

            //Attempt to login.
            await client.LoginAsync(TokenType.Bot, token);
            await client.StartAsync();

            await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

            await Task.Delay(Timeout.Infinite);
        }

        private static Task LogAsync(LogMessage log)
        {
            Console.WriteLine(log.ToString());
            return Task.CompletedTask;
        }

        private static ServiceProvider ConfigureServices() =>
        new ServiceCollection()
            .AddSingleton<DiscordSocketClient>()
            .AddSingleton<CommandService>()
            .AddSingleton<CommandHandlingService>()
            .AddSingleton<HttpClient>()
            .AddSingleton<CampBuddyCharacterService>()
            .AddSingleton<PictureService>()
                .BuildServiceProvider();
    }
}
