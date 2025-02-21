using System.Collections.Generic;
using System.Linq;

namespace ColorCode.Core
{
    /// <summary>
    /// Provides access to predefined programming languages for syntax highlighting.
    /// </summary>
    public static class Languages
    {
        // Static collection of supported languages
        private static readonly IReadOnlyList<ILanguage> _languages = new List<ILanguage>
        {
            new Language
            {
                Id = "csharp",
                Name = "C#",
                FirstLinePattern = null, // Optional: for detecting language in multi-language files
                Rules = new List<LanguageRule>
                {
                    // Example rule for C# (simplified)
                    new LanguageRule(
                        @"(//.*?$)|(/\*.*?\*/)",
                        new Dictionary<int, Color>
                        {
                            { 1, Color.FromArgb(0, 128, 0) }, // Green for single-line comments
                            { 2, Color.FromArgb(0, 128, 0) }  // Green for multi-line comments
                        }
                    )
                    // Add more rules for keywords, strings, etc.
                }
            },
            new Language
            {
                Id = "javascript",
                Name = "JavaScript",
                FirstLinePattern = null,
                Rules = new List<LanguageRule>
                {
                    // Example rule for JavaScript
                    new LanguageRule(
                        @"(//.*?$)|(/\*.*?\*/)",
                        new Dictionary<int, Color>
                        {
                            { 1, Color.FromArgb(0, 128, 0) },
                            { 2, Color.FromArgb(0, 128, 0) }
                        }
                    )
                }
            },
            // Add more languages as needed (e.g., Python, HTML, CSS, etc.)
        };

        /// <summary>
        /// Finds a language by its ID (e.g., "csharp", "javascript").
        /// </summary>
        /// <param name="id">The language ID to search for.</param>
        /// <returns>The matching ILanguage, or null if not found.</returns>
        public static ILanguage FindById(string id)
        {
            return _languages.FirstOrDefault(lang => lang.Id.Equals(id?.ToLowerInvariant()));
        }

        /// <summary>
        /// Gets the C# language definition.
        /// </summary>
        public static ILanguage CSharp => FindById("csharp");

        /// <summary>
        /// Gets all supported languages.
        /// </summary>
        public static IReadOnlyList<ILanguage> All => _languages;
    }

    /// <summary>
    /// Interface for language definitions used in syntax highlighting.
    /// </summary>
    public interface ILanguage
    {
        string Id { get; }
        string Name { get; }
        string FirstLinePattern { get; }
        IList<LanguageRule> Rules { get; }
    }

    /// <summary>
    /// Represents a language with its syntax highlighting rules.
    /// </summary>
    public class Language : ILanguage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstLinePattern { get; set; }
        public IList<LanguageRule> Rules { get; set; } = new List<LanguageRule>();
    }

    /// <summary>
    /// Defines a syntax highlighting rule for a language.
    /// </summary>
    public class LanguageRule
    {
        public LanguageRule(string regex, IDictionary<int, Color> captures)
        {
            Regex = regex;
            Captures = captures;
        }

        public string Regex { get; }
        public IDictionary<int, Color> Captures { get; }
    }

    /// <summary>
    /// Represents a color used for syntax highlighting.
    /// </summary>
    public struct Color
    {
        public byte R { get; }
        public byte G { get; }
        public byte B { get; }

        public static Color FromArgb(int r, int g, int b)
        {
            return new Color((byte)r, (byte)g, (byte)b);
        }

        private Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
    }
}