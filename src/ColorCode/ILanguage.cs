// ILanguage.cs
using System.Collections.Generic;

namespace ColorCode.Custom
{
    public interface ILanguage
    {
        string Id { get; }
        string Name { get; }
        IEnumerable<LanguageRule> Rules { get; }

        IEnumerable<Token> GetFormattedTokens(string source);
    }
}