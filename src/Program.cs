using System;
using System.IO;
using System.CommandLine;
using System.Threading.Tasks;

class Program
{
    static async Task<int> Main(string[] args)
    {
        var rootCommand = new RootCommand("Convert code files to images and manage them");

        // Convert command
        var convertCommand = new Command("convert", "Convert a code file to an image")
        {
            new Argument<string>("input", description: "Input code file path"),
            new Option<string>(new[] { "--output", "-o" }, description: "Output image file path") { DefaultValue = "output.png" }
        };
        convertCommand.SetHandler(async (context) =>
        {
            string input = context.ParseResult.GetValueForArgument<string>("input");
            string output = context.ParseResult.GetValueForOption<string>("output");

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Error: Input file is required for 'convert' command.");
                return 1;
            }

            var converter = new CodeToImageConverter();
            await converter.ConvertCodeToImage(input, output ?? Path.ChangeExtension(input, ".png"));
            Console.WriteLine($"Image saved as {Path.GetFullPath(output ?? Path.ChangeExtension(input, ".png"))}");
            return 0;
        });

        // Copy command
        var copyCommand = new Command("copy", "Copy all generated images to the clipboard");
        copyCommand.SetHandler(async (context) =>
        {
            var history = HistoryManager.LoadHistory();
            if (history.Count == 0)
            {
                Console.WriteLine("No images in history");
                return 0;
            }

            ClipboardHelper.CopyMultipleImagesToClipboard(history);
            Console.WriteLine("Note: All images have been copied to the clipboard for pasting into a web upload control.");
            return 0;
        });

        rootCommand.Add(convertCommand);
        rootCommand.Add(copyCommand);

        return await rootCommand.InvokeAsync(args);
    }
}