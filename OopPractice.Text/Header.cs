using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents a header (a container node).
    /// </summary>
    public class Header : Container
    {
        public Header(string name, Container? parent) : base(name, parent) { }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }
    }
}