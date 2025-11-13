using System.Text;

/// <summary>
/// Represents a composite text document, acting as a container for <see cref="TextElement"/> objects.
/// </summary>
namespace OopPractice.Text
{
    public class Text
    {
        /// <summary>
        /// Stores all elements of the document in order.
        /// </summary>
        private readonly List<TextElement> _elements = new List<TextElement>();

        /// <summary>
        /// Adds a new element to the document.
        /// </summary>
        /// <param name="element">The text element to add.</param>
        public void Add(TextElement element)
        {
            _elements.Add(element);
        }

        /// <summary>
        /// Adds a new element to the document.
        /// </summary>
        /// <param name="index">The id element to remove.</param>
        public void Remove(int index)
        {
            _elements.RemoveAt(index);
        }


        /// <summary>
        /// Renders the full text of the document.
        /// </summary>
        /// <returns>The complete rendered document as a string.</returns>
        public string RenderFullText()
        {
            var stringBuilder = new StringBuilder();

            foreach (var element in _elements)
            {
                stringBuilder.Append(element.Render());
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Renders the Table of Contents for the document.
        /// </summary>
        /// <returns>A string representing the table of contents.</returns>
        public string RenderTableOfContents()
        {
            var stringBuilder = new StringBuilder("--- Table of Contents ---\n");

            foreach (var element in _elements)
            {
                var header = element.GetContentHeader();

                if (header != null)
                {
                    stringBuilder.AppendLine(header);
                }
            }

            stringBuilder.Append("---------------------------\n");
            return stringBuilder.ToString();
        }
    }
}