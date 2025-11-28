using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents a paragraph (a leaf node).
    /// </summary>
    public class Paragraph : Leaf
    {
        public Paragraph(string? content) : base(content) { }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}