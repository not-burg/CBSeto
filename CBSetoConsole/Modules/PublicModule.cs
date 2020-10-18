using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using System.Reflection;
using System.Text;
using CBSetoLib.Services;
using Discord.WebSocket;

namespace CBSetoConsole.Modules
{
    public class PublicModule : ModuleBase<SocketCommandContext>
    {
        public CampBuddyCharacterService CharacterService { get; set; }
        public TimeZoneService TimeZoneService { get; set; }
        public PictureService PictureService { get; set; }
        private readonly Uri _kittyUri = new Uri("https://cataas.com/cat");
        private readonly Uri _bottomThoughts = new Uri("https://cdn.discordapp.com/attachments/746301814214754315/767061098070278144/image0.png");

        [Command("Character")] [Summary("Retrieves info about a camper.")]
        public async Task CharacterImageAsync([Remainder] string name)
        {
            if (CharacterService.TryGetCharacter(name, out var character) == false)
            {
                await ReplyAsync("Unfortunately there's no camper by that name :("); 
                return;
            }

            var embedBuilder = new EmbedBuilder { Title = $"{character.Info.Name} (#{character.Id})" };
            embedBuilder.AddField("Height: ", character.Info.Height + "cm", true);
            embedBuilder.AddField("Weight: ", character.Info.Weight + "kg", true);
            embedBuilder.ImageUrl = character.ImageUri.AbsoluteUri;
            embedBuilder.AddField("Learn more: ", character.ProfileUri);

            await ReplyAsync(embed: embedBuilder.Build());
        }

        [Command("UserInfo")] [Summary("Retrieves info about a user.")]
        public async Task UserInfoAsync(IUser user = null)
        {
            user ??= Context.User;

            var embedBuilder = new EmbedBuilder
            {
                Title = $"{user.Username}#{user.Discriminator}",
                ImageUrl = user.GetAvatarUrl() 
            };
            embedBuilder.AddField("Id: ", user.Id);
            embedBuilder.AddField("Created Date: ", user.CreatedAt.UtcDateTime.ToLongDateString());

            await ReplyAsync(embed: embedBuilder.Build());
        }

        [Command("BottomThoughts")] [Summary("All I have are bottom thoughts.")]
        public async Task BottomThoughts()
        {
            var bottomThoughts = await PictureService.GetPictureAsync(_bottomThoughts);
            await Context.Channel.SendFileAsync(bottomThoughts, "bottomThoughts.png");
        }

        [Command("Ban")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.BanMembers)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        [Summary("Banks a specified user for a specified reason (if given).")]
        public async Task BanUserAsync(IGuildUser user, [Remainder] string reason = null)
        {
            await user.Guild.AddBanAsync(user, reason: reason);
            await ReplyAsync($"User {user.Username}#{user.Discriminator} ({user.Id}) has been banned.");
        }

        [Command("Sweep")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        [RequireBotPermission(GuildPermission.ManageMessages)]
        [Summary("Deletes a specified number of messages in the channel.")]
        public async Task Sweep([Remainder] int messageCount)
        {
            if (messageCount <= 0)
            {
                await ReplyAsync("Silly, number of messages to delete needs to be >= 0");
                return;
            }
            var deletionTasks = new List<Task>(messageCount / 100);

            int messagedDeleted = 0;
            if (messageCount >= 99)
            {
                int hundreds = messageCount / 100;
                for (int i = 1; i < hundreds + 1; i++)
                {
                    var messages = await Context.Channel.GetMessagesAsync().FlattenAsync();
                    deletionTasks.Add(DeleteMessagesAsync(messages));
                    messagedDeleted += 100;
                }
            }
            int remainder = messageCount % 100;
            var remainingMessages = await Context.Channel.GetMessagesAsync(remainder).FlattenAsync();
            deletionTasks.Add(DeleteMessagesAsync(remainingMessages));
            messagedDeleted += remainder;

            await Task.WhenAll(deletionTasks);
            await ReplyAsync($"🧹 Deleted {messagedDeleted} messages.");
        }

        private Task DeleteMessagesAsync(IEnumerable<IMessage> messages) => 
            (Context.Channel as SocketTextChannel).DeleteMessagesAsync(messages);

        [Command("Echo")] [Summary("Repeats a user's message.")]
        public Task EchoAsync([Remainder] string text) => ReplyAsync('\u200B' + text);

        [Command("Timezones")] [Summary("Shows time in common timezones.")]
        public Task TimezonesCommand()
        {
            var timeZoneGroups = TimeZoneService.GetTimeKeyValuePairs().GroupBy(
                timeZone => timeZone.Value,
                timeZone => timeZone.Key,
                (time, timeZones) => new KeyValuePair<string, string>(time, string.Join('\n', timeZones)));

            var embedBuilder = new EmbedBuilder { Title = "Current time in common timezones: " };
            foreach (var (time, timeZones) in timeZoneGroups)
                embedBuilder.AddField(time, timeZones, true);

            return ReplyAsync(embed: embedBuilder.Build());
        }

        [Command("Help")] [Alias("help")]
        public Task HelpCommandAsync()
        {
            var embedBuilder = new EmbedBuilder {Title = "Commands: "};

            var methods = typeof(PublicModule).GetMethods()
                .Where(m => m.IsPublic && m.GetCustomAttributes<CommandAttribute>(true).Any()).Select(info => new 
                {

                });

            return ReplyAsync(embed: embedBuilder.Build());
        }

        [Command("Kitty")] [Alias("kitty")] [Summary("Retrieves a random cat image from https://cataas.com/cat.")]
        public async Task KittyCommandAsync()
        {
            var kittyImage = await PictureService.GetPictureAsync(_kittyUri);
            await Context.Channel.SendFileAsync(kittyImage, "kitty.png");
        }

        [Command("Reflect")] [Summary("Shows the underlying programming metadata of a specified command.")]
        public Task Reflect([Remainder] string targetCommand)
        {
            var methods = typeof(PublicModule)
                .GetMethods()
                .Where(m => m.IsPublic && m.GetCustomAttributes<CommandAttribute>(true).Any());

            var method = methods.FirstOrDefault(m => m.GetCustomAttribute<CommandAttribute>(true).Text.ToLower().Contains(targetCommand.ToLower()));

            if (method is null) 
                return ReplyAsync("No method found");

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

            string replyStr = reply.ToString();
            return replyStr.Length > 2000 ? 
                ReplyAsync(string.Concat(replyStr.Take(1997)) + "...") : 
                ReplyAsync(replyStr);
        }
    }
}
