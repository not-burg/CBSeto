using Discord;
using Discord.WebSocket;

namespace CBSetoConsole.Extensions
{
    public static class MessageExtensions
    {
        public static bool IsUserMessage(this IMessage message)
        {
            return message is SocketUserMessage && message.Source == MessageSource.User;
        }

        public static bool IsUserMessage(this IMessage message, out SocketUserMessage userMessage)
        {
            userMessage = message as SocketUserMessage;
            return userMessage.IsUserMessage();
        }
    }
}