using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents an abstract leaf node in the text composite structure.
    /// It cannot have children.
    /// </summary>
    public abstract class Leaf : IText
    {
        public Guid Id { get; } = Guid.NewGuid();
        protected readonly string? _content;

        protected Leaf(string? content)
        {
            _content = content;
        }

        public abstract void Render(StringBuilder builder, int indentation, bool showIds);

        public override string ToString()
        {
            return $"[Leaf] {_content}";
        }
    }
}