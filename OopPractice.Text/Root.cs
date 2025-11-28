using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents the root of the document.
    /// </summary>
    public class Root : Container
    {
        public Root(string title) : base(title, null) { }

        public bool IsRoot() => true;

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
            base.Accept(visitor);
        }
    }
}