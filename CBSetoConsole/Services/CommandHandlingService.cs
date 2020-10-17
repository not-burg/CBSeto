using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CBSetoConsole.Extensions;
using CBSetoLib.Helpers;
using Microsoft.Extensions.DependencyInjection;

using CBSetoLib.Services;

using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace CBSetoConsole.Services
{
    public class CommandHandlingService
    {
        private readonly CommandService _commands;
        private readonly DiscordSocketClient _discord;
        private readonly IServiceProvider _services;
        private readonly CampBuddyCharacterService _characterService;

        public CommandHandlingService(IServiceProvider services)
        {
            _services = services;

            _discord = _services.GetRequiredService<DiscordSocketClient>();
            _commands = _services.GetRequiredService<CommandService>();
            _characterService = _services.GetRequiredService<CampBuddyCharacterService>();

            _commands.CommandExecuted += CommandExecutedAsync;
            _discord.MessageReceived += MessageReceivedAsync;
        }

        public async Task InitializeAsync() => 
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        private async Task MessageReceivedAsync(SocketMessage message)
        {
            if (message.IsUserMessage(out var userMessage) == false) return;

            var tasks = new List<Task> { AddCharacterReactions(userMessage) };

            //Tries to parse and execute a command if the userMessage has a mention prefix to the bot.
            int argPos = "s!".Length;
            if (userMessage.HasStringPrefix("s!", ref argPos) == false) return;
            var context = new SocketCommandContext(_discord, userMessage);
            tasks.Add(_commands.ExecuteAsync(context, argPos, _services));

            await Task.WhenAll(tasks);
        }

        private async Task CommandExecutedAsync(Optional<CommandInfo> command, 
            ICommandContext context, IResult result)
        {
            if (command.IsSpecified == false || result.IsSuccess) return;
            await context.Channel.SendMessageAsync($"Command failed: {result.ErrorReason}");
        }

        private async Task AddCharacterReactions(IMessage tgtMsg)
        {
            if (ParsingHelper.WordsMatchWordSet(tgtMsg.ToString(), _characterService.GetCharacterNames(), out var characterNames))
                await Task.WhenAll(GetReactionTasks(GetCharacterEmoji(characterNames), tgtMsg));
        }

        private static IEnumerable<Task> GetReactionTasks(IEnumerable<Emoji> emojis, IMessage msg) => 
            emojis.Select(emoji => msg.AddReactionAsync(emoji));

        private IEnumerable<Emoji> GetCharacterEmoji([NotNull] IEnumerable<string> names)
        {
            foreach (var name in names)
                if (_characterService.ContainsCharacterEmote(name, out var emote))
                    yield return new Emoji(emote);
        }
    }
}