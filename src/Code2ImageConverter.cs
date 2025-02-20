using ColorCode;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public class CodeToImageConverter
{
    private const int Padding = 20;
    private const int FontSize = 16;
    private const float LineHeight = 1.5f;
    private const string BackgroundColor = "#1E1E1E";
    private const string FontFamily = "Consolas";
    private const int MaxWidth = 800;

    public async Task ConvertCodeToImage(string inputFile, string outputFile)
    {
        try
        {
            // Read the code file
            string code = await File.ReadAllTextAsync(inputFile);
            string extension = Path.GetExtension(inputFile).TrimStart('.').ToLower();
            string language = MapLanguage(extension);

            // Highlight the code using ColorCode.Core
            var formatter = new HtmlFormatter();
            string highlighted = formatter.GetHtmlString(code, Languages.FindById(language) ?? Languages.CSharp, StyleSheets.DefaultDark);

            // Parse HTML to extract spans (simplified, as ColorCode outputs HTML)
            highlighted = Regex.Replace(highlighted, "<[^>]+>", match =>
            {
                if (match.Value.Contains("class="))
                {
                    string className = match.Value.Split('"')[1];
                    return GetColorSpan(className);
                }
                return "";
            });

            // Split into lines
            string[] lines = highlighted.Split('\n');

            // Create bitmap for drawing
            float lineHeightPx = FontSize * LineHeight;
            int canvasHeight = (int)(lines.Length * lineHeightPx + Padding * 2);
            int canvasWidth = MaxWidth;

            using Bitmap bitmap = new Bitmap(canvasWidth, canvasHeight);
            using Graphics graphics = Graphics.FromImage(bitmap);
            using Font font = new Font(FontFamily, FontSize);

            // Set background color
            graphics.Clear(ColorTranslator.FromHtml(BackgroundColor));

            // Draw each line with colors
            float x = Padding;
            float y = Padding;
            Dictionary<string, Color> colors = new()
            {
                { "keyword", Color.FromArgb(86, 156, 214) },
                { "string", Color.FromArgb(206, 145, 120) },
                { "comment", Color.FromArgb(106, 153, 85) },
                { "number", Color.FromArgb(181, 206, 168) },
                { "function", Color.FromArgb(220, 220, 170) }
            };

            foreach (string line in lines)
            {
                string[] parts = Regex.Split(line, "(<span[^>]*>|</span>)");
                foreach (string part in parts)
                {
                    if (part.StartsWith("<span class="))
                    {
                        string className = part.Split('"')[1].Replace("hljs-", "");
                        graphics.DrawString(part, font, new SolidBrush(colors.ContainsKey(className) ? colors[className] : Color.White), x, y);
                    }
                    else if (!part.StartsWith("</span>"))
                    {
                        SizeF size = graphics.MeasureString(part, font);
                        graphics.DrawString(part, font, Brushes.White, x, y);
                        x += size.Width;
                    }
                }
                x = Padding;
                y += lineHeightPx;
            }

            // Save the image
            bitmap.Save(outputFile, System.Drawing.Imaging.ImageFormat.Png);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            throw;
        }
    }

    private string MapLanguage(string extension)
    {
        return extension switch
        {
            "js" => "javascript",
            "py" => "python",
            "java" => "java",
            "cpp" => "cpp",
            "c" => "c",
            _ => "javascript"
        };
    }

    private string GetColorSpan(string className)
    {
        // Simplified mapping of classes to colors (for drawing)
        return $"<span style=\"color: {MapColor(className)}\">";
    }

    private string MapColor(string className)
    {
        return className switch
        {
            "keyword" => "#569CD6",
            "string" => "#CE9178",
            "comment" => "#6A9955",
            "number" => "#B5CEA8",
            "function" => "#DCDCAA",
            _ => "#FFFFFF"
        };
    }
}