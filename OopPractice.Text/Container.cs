using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents an abstract container node in the text composite structure.
    /// It can contain other IText components.
    /// </summary>
    public abstract class Container : IText
    {
        protected readonly string _name;
        protected readonly List<IText> _children = new List<IText>();

        public Container? Parent { get; private set; }

        protected Container(string name, Container? parent = null)
        {
            _name = name;
            Parent = parent;
        }

        public void AddChild(IText child)
        {
            _children.Add(child);

            if (child is Container container)
            {
                container.Parent = this;
            }
        }

        public virtual void Render(StringBuilder builder, int indentation)
        {
            foreach (var child in _children)
            {
                child.Render(builder, indentation + 1);
            }
        }
    }
}