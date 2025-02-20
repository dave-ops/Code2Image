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
        var inputArgument = new Argument<string>("input", description: "Input code file path");
        var outputOption = new Option<string>(
            name: "--output",
            description: "Output image file path",
            getDefaultValue: () => "output.png"); // Use getDefaultValue instead of DefaultValue

        var convertCommand = new Command("convert", "Convert a code file to an image");
        convertCommand.AddArgument(inputArgument);
        convertCommand.AddOption(outputOption);

        convertCommand.SetHandler(async (input, output) =>
        {
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Error: Input file is required for 'convert' command.");
                return; // No return value needed for void handler
            }

            var converter = new CodeToImageConverter();
            await converter.ConvertCodeToImage(input, output ?? Path.ChangeExtension(input, ".png"));
            Console.WriteLine($"Image saved as {Path.GetFullPath(output ?? Path.ChangeExtension(input, ".png"))}");
        }, inputArgument, outputOption);

        // Copy command
        var copyCommand = new Command("copy", "Copy all generated images to the clipboard");
        copyCommand.SetHandler(async () =>
        {
            var history = HistoryManager.LoadHistory();
            if (history.Count == 0)
            {
                Console.WriteLine("No images in history");
                return; // No return value needed for void handler
            }

            ClipboardHelper.CopyMultipleImagesToClipboard(history);
            Console.WriteLine("Note: All images have been copied to the clipboard for pasting into a web upload control.");
        });

        rootCommand.Add(convertCommand);
        rootCommand.Add(copyCommand);

        return await rootCommand.InvokeAsync(args);
    }
}