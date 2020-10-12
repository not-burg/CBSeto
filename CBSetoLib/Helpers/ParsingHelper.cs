using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBSetoLib.Helpers
{
    public static class ParsingHelper
    {
        public static bool WordsMatchWordSet([NotNull] string text, 
            IEnumerable<string> characterNames, 
            out IReadOnlyCollection<string> matchingWords) {

            matchingWords = text.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Distinct().Where(characterNames.Contains).ToArray();

            return matchingWords.Any();
        }
    }
}
