using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Manages the creation of a text document using a factory pattern.
    /// Simplifies the process of building a composite text structure.
    /// </summary>
    public class TextFactory
    {
        /// <summary>
        /// Gets the root node of the document.
        /// </summary>
        public Root RootNode { get; }

        /// <summary>
        /// Gets the current container node the factory is adding to.
        /// </summary>
        public Container CurrentNode { get; private set; }

        /// <summary>
        /// Initializes a new factory for a document.
        /// </summary>
        /// <param name="title">The main title of the document.</param>
        public TextFactory(string title)
        {
            RootNode = new Root(title);
            CurrentNode = RootNode;
        }

        /// <summary>
        /// Adds a new header at the current level and makes it the new current container.
        /// </summary>
        /// <param name="name">The text of the header.</param>
        public void AddHeading(string name)
        {
            var heading = new Header(name, CurrentNode); 
            CurrentNode.AddChild(heading); 
            CurrentNode = heading; 
        }

        /// <summary>
        /// Adds a paragraph at the current level.
        /// </summary>
        /// <param name="content">The text of the paragraph.</param>
        public void AddParagraph(string? content)
        {
            CurrentNode.AddChild(new Paragraph(content)); 
        }

        /// <summary>
        /// Moves the factory's context "up" one level to the parent container.
        /// </summary>
        public void Up()
        {
            if (CurrentNode.Parent != null)
            {
                CurrentNode = CurrentNode.Parent;
            }
        }

        /// <summary>
        /// Returns the final rendered string for the entire document.
        /// </summary>
        public override string ToString()
        {
            var visitor = new PlainTextVisitor(false);
            RootNode.Accept(visitor);
            return visitor.GetResult();
        }

        public string RenderWithIds()
        {
            var visitor = new PlainTextVisitor(true);
            RootNode.Accept(visitor);
            return visitor.GetResult();
        }

        /// <summary>
        /// Manually sets the current working node.
        /// </summary>
        public void SetCurrentNode(Container container)
        {
            CurrentNode = container;
        }
    }
}