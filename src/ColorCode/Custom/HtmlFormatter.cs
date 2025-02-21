// HtmlFormatter.cs
using System.Text;

namespace ColorCode.Custom
{
    public class HtmlFormatter
    {
        public string GetHtmlString(string code, ILanguage language, IStyleSheet styleSheet)
        {
            if (string.IsNullOrEmpty(code))
                return string.Empty;

            if (language == null)
                throw new ArgumentNullException(nameof(language), "Language cannot be null.");

            if (styleSheet == null)
                throw new ArgumentNullException(nameof(styleSheet), "StyleSheet cannot be null.");

            StringBuilder html = new StringBuilder();
            html.AppendLine("<pre><code>");

            var tokens = language.GetFormattedTokens(code);
            foreach (var token in tokens)
            {
                string className = styleSheet.GetClassName(token.Scope);
                if (!string.IsNullOrEmpty(className))
                {
                    html.Append($"<span class=\"{className}\">{HtmlEncode(token.Text)}</span>");
                }
                else
                {
                    html.Append(HtmlEncode(token.Text));
                }
            }

            html.AppendLine("</code></pre>");
            return html.ToString();
        }

        private string HtmlEncode(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return System.Web.HttpUtility.HtmlEncode(text)
                .Replace("\r\n", "<br>")
                .Replace("\n", "<br>");
        }
    }
}