using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents an abstract leaf node in the text composite structure.
    /// It cannot have children.
    /// </summary>
    public abstract class Leaf : IText
    {
        protected readonly string? _content;

        protected Leaf(string? content)
        {
            _content = content;
        }

        public abstract void Render(StringBuilder builder, int indentation);
    }
}