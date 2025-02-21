using ColorCode;
using System.Collections.Generic;
using System.Drawing;

namespace ColorCode.Custom
{
    public class DefaultDarkStyleSheet : IStyleSheet
    {
        private readonly Dictionary<string, Style> _styles;

        public DefaultDarkStyleSheet()
        {
            _styles = new Dictionary<string, Style>
            {
                // Define styles for DefaultDark, using properties or correct constructor
                { "plain", new Style { Foreground = Brushes.White, FontStyle = FontStyle.Regular } }, // Default text (white)
                { "keyword", new Style { Foreground = Brushes.FromArgb(86, 156, 214), FontStyle = FontStyle.Regular } }, // Blue for keywords
                { "string", new Style { Foreground = Brushes.FromArgb(206, 145, 120), FontStyle = FontStyle.Regular } }, // Orange for strings
                { "comment", new Style { Foreground = Brushes.FromArgb(106, 153, 85), FontStyle = FontStyle.Italic } }, // Green for comments (italic)
                { "number", new Style { Foreground = Brushes.FromArgb(181, 206, 168), FontStyle = FontStyle.Regular } }, // Light green for numbers
                { "function", new Style { Foreground = Brushes.FromArgb(220, 220, 170), FontStyle = FontStyle.Regular } } // Yellow for functions
            };
        }

        public IEnumerable<KeyValuePair<string, Style>> Styles => _styles;

        public string GetClassName(string scopeName)
        {
            return _styles.ContainsKey(scopeName) ? scopeName : "plain";
        }
    }
}