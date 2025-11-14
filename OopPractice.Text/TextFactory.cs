using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Manages the creation of a text document using a factory pattern.
    /// Simplifies the process of building a composite text structure.
    /// </summary>
    public class TextFactory
    {
        private readonly Root _root;
        private Container _current;

        /// <summary>
        /// Initializes a new factory for a document.
        /// </summary>
        /// <param name="title">The main title of the document.</param>
        public TextFactory(string title)
        {
            _root = new Root(title);
            _current = _root;
        }

        /// <summary>
        /// Adds a new header at the current level and makes it the new current container.
        /// </summary>
        /// <param name="name">The text of the header.</param>
        public void AddHeading(string name)
        {
            var heading = new Header(name, _current);
            _current.AddChild(heading);
            _current = heading;
        }

        /// <summary>
        /// Adds a paragraph at the current level.
        /// </summary>
        /// <param name="content">The text of the paragraph.</param>
        public void AddParagraph(string? content)
        {
            _current.AddChild(new Paragraph(content));
        }

        /// <summary>
        /// Moves the factory's context "up" one level to the parent container.
        /// </summary>
        public void Up()
        {
            if (_current.Parent != null)
            {
                _current = _current.Parent;
            }
            // If Parent is null, we are already at the root, do nothing.
        }

        /// <summary>
        /// Returns the final rendered string for the entire document.
        /// </summary>
        public override string ToString()
        {
            // The Root class has a helper method for this
            return _root.RenderToString();
        }
    }
}