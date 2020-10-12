using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Reflection;
using CBSetoLib.Services;

namespace CBSetoConsole.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        //Set by Dependency Injection
        public CampBuddyCharacterService CharacterService { get; set; }
        public PictureService PictureService { get; set; }

        [Command("Character")] [Alias("character")]
        public async Task CharacterImageAsync([Remainder] string name)
        {
            if (CharacterService.ContainsCharacterImage(name, out var uri) == false) return;
            var stream = await PictureService.GetPictureAsync(uri);
            stream.Seek(0, SeekOrigin.Begin);
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
        public Task FormatCSharpAsync(params string[] codeLines)
        {
            string formattedCode = string.Join('\n', "**C#**", "```CS", codeLines, "```");
            return ReplyAsync(formattedCode);
        }

        [Command("Help")] [Alias("help")]
        public Task HelpCommandAsync()
        {
            var commands = GetCommandAttributesText();
            return ReplyAsync(string.Join('\n', commands));
        }

        private static IEnumerable<string> GetCommandAttributesText() =>
            typeof(PublicModule).GetMethods()
                .Where(method => method.IsPublic && method.ReturnType == typeof(Task))
                .SelectMany(method => method.GetCustomAttributes<CommandAttribute>(true))
                .Select(commandAttribute => commandAttribute.Text);
    }
}
