using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents a base abstract element that can be part of a text document.
    /// </summary>
    public abstract class TextElement
    {
        /// <summary>
        /// Renders the element into its string representation (e.g., plain text, HTML, Markdown).
        /// </summary>
        /// <returns>A string representation of the element.</returns>
        public abstract string Render();

        /// <summary>
        /// Gets the text for the Table of Contents.
        /// </summary>
        /// <returns>A string if the element is a header, otherwise null.</returns>
        public virtual string? GetContentHeader()
        {
            return null;
        }
    }
}
