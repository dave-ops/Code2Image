using ColorCode;
using ColorCode.Common;
using System.Collections.Generic;
using System.Drawing;

namespace ColorCode
{
    public class DefaultDarkStyleSheet : IStyleSheet
    {
        private readonly Dictionary<string, Style> _styles;

        public DefaultDarkStyleSheet()
        {
            _styles = new Dictionary<string, Style>
            {
                { "plain", CreateStyle("plain", Color.White, FontStyle.Regular) },
                { "keyword", CreateStyle("keyword", Color.FromArgb(86, 156, 214), FontStyle.Regular) },
                { "string", CreateStyle("string", Color.FromArgb(206, 145, 120), FontStyle.Regular) },
                { "comment", CreateStyle("comment", Color.FromArgb(106, 153, 85), FontStyle.Italic) },
                { "number", CreateStyle("number", Color.FromArgb(181, 206, 168), FontStyle.Regular) },
                { "function", CreateStyle("function", Color.FromArgb(220, 220, 170), FontStyle.Regular) }
            };
        }

        private Style CreateStyle(string scopeName, Color color, FontStyle fontStyle)
        {
            var style = new Style(scopeName);
            style.SetForegroundFromBrush(new SolidBrush(color));
            style.FontStyle = fontStyle;
            return style;
        }

        public IEnumerable<KeyValuePair<string, Style>> Styles => _styles;

        public string GetClassName(string scopeName)
        {
            return _styles.ContainsKey(scopeName) ? scopeName : "plain";
        }
    }
}
