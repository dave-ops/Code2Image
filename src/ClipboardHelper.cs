using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

public static class ClipboardHelper
{
    public static void CopyImageToClipboard(string imagePath)
    {
        try
        {
            using Bitmap bitmap = new Bitmap(imagePath);
            Clipboard.SetImage(bitmap);
            Console.WriteLine($"Image {imagePath} copied to clipboard successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying image to clipboard: {ex.Message}");
        }
    }

    public static void CopyMultipleImagesToClipboard(List<string> imagePaths)
    {
        try
        {
            var dataObject = new DataObject();

            // Add each image as a file reference (FileDrop format)
            string[] files = imagePaths.ToArray();
            dataObject.SetData(DataFormats.FileDrop, files);

            // Optionally, add the images as bitmaps for direct pasting
            foreach (string path in imagePaths)
            {
                using Bitmap bitmap = new Bitmap(path);
                dataObject.SetImage(bitmap);
            }

            Clipboard.SetDataObject(dataObject, true);
            Console.WriteLine($"Copied {imagePaths.Count} images to clipboard successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error copying images to clipboard: {ex.Message}");
            throw;
        }
    }
}