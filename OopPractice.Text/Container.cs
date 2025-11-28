using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents an abstract container node in the text composite structure.
    /// It can contain other IText components.
    /// </summary>
    public abstract class Container : IText
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; }
        protected readonly List<IText> _children = new List<IText>();

        public Container? Parent { get; private set; }

        protected Container(string name, Container? parent = null)
        {
            Name = name;
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

        /// <summary>
        /// Removes a child element from this container.
        /// </summary>
        /// <param name="child">The child element to remove.</param>
        /// <returns>True if removal was successful, false otherwise.</returns>
        public bool RemoveChild(IText child)
        {
            return _children.Remove(child);
        }

        public virtual void Accept(IVisitor visitor)
        {
            foreach (var child in _children)
            {
                child.Accept(visitor);
            }
        }
        public override string ToString()
        {
            return $"[Container] {Name} ({_children.Count} children)";
        }


        /// <summary>
        /// Finds a child element by its name (if it's a Container) or content (if it's a Leaf).
        /// This is a simplified search.
        /// </summary>
        public IText? FindChild(string nameOrContent)
        {
            return _children.FirstOrDefault(child =>
            {
                if (child is Container c)
                {
                    return c.Name.Equals(nameOrContent, StringComparison.OrdinalIgnoreCase);
                }
                if (child is Leaf l)
                {
                    return (l.Content ?? string.Empty).Trim().Equals(nameOrContent, StringComparison.OrdinalIgnoreCase);
                }
                return false;
            });
        }
    }
}