using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public static class HistoryManager
{
    private static string HistoryFile => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "image-history.json");

    public static List<string> LoadHistory()
    {
        if (File.Exists(HistoryFile))
        {
            string json = File.ReadAllText(HistoryFile);
            return JsonConvert.DeserializeObject<List<string>>(json) ?? new List<string>();
        }
        return new List<string>();
    }

    public static void SaveHistory(List<string> history)
    {
        File.WriteAllText(HistoryFile, JsonConvert.SerializeObject(history, Formatting.Indented));
    }
}