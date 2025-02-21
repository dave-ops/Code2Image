using System.Drawing;

namespace ColorCode.Custom
{
    public class Style
    {
        public string ScopeName { get; }
        public string Foreground { get; set; } // CSS color (e.g., "#FFFFFF")
        public FontStyle? FontStyle { get; set; }

        public Style(string scopeName)
        {
            ScopeName = scopeName ?? throw new ArgumentNullException(nameof(scopeName));
        }

        // Helper method to convert Brush to CSS color string
        public void SetForegroundFromBrush(Brush brush)
        {
            if (brush is SolidBrush solidBrush)
            {
                Foreground = $"#{solidBrush.Color.R:X2}{solidBrush.Color.G:X2}{solidBrush.Color.B:X2}";
            }
            else
            {
                throw new NotSupportedException("Only SolidBrush is supported for color conversion.");
            }
        }
    }
}