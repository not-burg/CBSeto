using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Reflection;
using System.Text;
using CBSetoLib.Services;

namespace CBSetoConsole.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public CampBuddyCharacterService CharacterService { get; set; }
        public TimeZoneService TimeZoneService { get; set; }
        public PictureService PictureService { get; set; }
        private readonly Uri _kittyUri = new Uri("https://cataas.com/cat");

        [Command("Character")] [Alias("character")]
        public async Task CharacterImageAsync([Remainder] string name)
        {
            if (CharacterService.ContainsCharacterImage(name, out var uri) == false) return;
            var stream = await PictureService.GetPictureAsync(uri);
            await Context.Channel.SendFileAsync(stream, $"{name}.png");
        }

        [Command("UserInfo")] [Alias("userinfo")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user ??= Context.User;

            var userInfo = string.Join('\n', 
                $"{user.Username}#{user.Discriminator}",
                user.CreatedAt.UtcDateTime.ToString("D"));

            await ReplyAsync(userInfo);
        }

        [Command("Ban")] [Alias("ban")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await user.Guild.AddBanAsync(user, reason: reason);
            await ReplyAsync($"User {user.Username}#{user.Discriminator} ({user.Id}) has been banned.");
        }

        [Command("Echo")] [Alias("echo")]
        public Task EchoAsync([Remainder] string text) => ReplyAsync('\u200B' + text);

        [Command("List")] [Alias("list")]
        public Task ListAsync(params string[] objects) => ReplyAsync("You listed: " + string.Join("; ", objects));

        [Command("GuildOnly")] [Alias("guildOnly")]
        [RequireContext(ContextType.Guild)]
        public Task GuildOnlyAsync() => ReplyAsync("Nothing to see here...yet");

        [Command("FormatC#")] [Alias("formatC#")]
        public Task FormatCSharpAsync(string text)
        {
            string formattedCode = string.Join('\n', "**C#**", "```CS", text, "```");
            return ReplyAsync(formattedCode);
        }

        [Command("Timezones")]
        [Alias("timezones")]
        public Task GetTimeZonesAsync()
        {
            return ReplyAsync(string.Join('\n', TimeZoneService.GetTimeStrings()));
        }

        [Command("Help")] [Alias("help")]
        public Task HelpCommandAsync()
        {
            var commands = GetCommandAttributesText();
            return ReplyAsync(string.Join('\n', commands));
        }

        [Command("Kitty")] [Alias("kitty")]
        public async Task KittyCommandAsync()
        {
            var kittyImage = await PictureService.GetPictureAsync(_kittyUri);
            await Context.Channel.SendFileAsync(kittyImage, "kitty.png");
        }

        [Command("RichEmbed")] [Alias("richEmbed")]
        public Task SendRichEmbed()
        {
            var embed = new EmbedBuilder
            {
                Title = "Hey, there, fellow camper!", 
                Description = "My name's Seto, I'll be your guide to Cabin 3"
            };
            embed.AddField("Find my profile", "https://www.blitsgames.com/characters/");

            return ReplyAsync(embed: embed.Build());
        }

        private static IEnumerable<string> GetCommandAttributesText() =>
            typeof(PublicModule).GetMethods()
                .Where(method => method.IsPublic && method.ReturnType == typeof(Task))
                .SelectMany(method => method.GetCustomAttributes<CommandAttribute>(true))
                .Select(commandAttribute => commandAttribute.Text);

        [Command("Reflect")]
        public Task Reflect([Remainder] string targetCommand)
        {
            var methods = typeof(PublicModule)
                .GetMethods()
                .Where(m => m.IsPublic && m.GetCustomAttributes<CommandAttribute>(true).Any());

            var method = methods.FirstOrDefault(m => m.GetCustomAttribute<CommandAttribute>(true).Text.ToLower().Contains(targetCommand.ToLower()));

            if (method is null) return ReplyAsync("No method found");

            var methodBody = method.GetMethodBody();

            var reply = new StringBuilder();
            reply.AppendLine("Method signature:");

            reply.AppendLine("```CS");
            reply.AppendLine(method.ToString());
            reply.AppendLine("```");

            var localVariables = methodBody.LocalVariables.Select(variable => $"{variable.LocalType?.Name}").ToList();
            if (localVariables.Any())
            {
                reply.AppendLine("Local variables:");
                reply.AppendLine("```CS");
                localVariables.ForEach(localVariable => reply.AppendLine(localVariable));
                reply.AppendLine("```");
            }
            
            reply.AppendLine("MSIL in hexadecimal:");

            reply.Append('`');
            reply.Append(string.Join(' ', methodBody.GetILAsByteArray().Select(il => il.ToString("X").PadRight(2,'0'))));
            reply.Append('`');

            return ReplyAsync(string.Concat(reply.ToString().Take(1997)) + "...");
        }
    }
}
