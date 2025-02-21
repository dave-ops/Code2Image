using System.Collections.Generic;
using System.Drawing;
using ColorCode.HTML;
using ColorCode.Styling;

namespace ColorCode
{
    public interface IStyleSheet
    {
        /// <summary>
        /// Gets the collection of styles defined by this style sheet.
        /// </summary>
        IEnumerable<KeyValuePair<string, Style>> Styles { get; }

        /// <summary>
        /// Gets the CSS class name for a given scope name.
        /// </summary>
        /// <param name="scopeName">The scope name to map to a CSS class.</param>
        /// <returns>The CSS class name for the scope, or null if no mapping exists.</returns>
        string GetClassName(string scopeName);
    }
}