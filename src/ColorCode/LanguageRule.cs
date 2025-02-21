// LanguageRule.cs
using System.Text.RegularExpressions;

namespace ColorCode
{
    public class LanguageRule
    {
        public Regex Regex { get; }
        public string ScopeName { get; }

        public LanguageRule(Regex regex, string scopeName)
        {
            Regex = regex ?? throw new ArgumentNullException(nameof(regex));
            ScopeName = scopeName ?? throw new ArgumentNullException(nameof(scopeName));
        }
    }
}