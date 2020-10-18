using System;
using System.Threading.Tasks;
using Discord.Commands;

namespace CBSetoConsole.Services
{
    class StringArrayTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            try
            {
                var lines = input.Trim().Split('\n');
                return Task.FromResult(TypeReaderResult.FromSuccess(lines));
            }
            catch (Exception exception)
            {
                return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, exception.Message));
            }
        }
    }
}
