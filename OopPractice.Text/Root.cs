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

        public override void Render(StringBuilder builder, int indentation)
        {
            builder.AppendLine(_name);
            base.Render(builder, indentation);
        }

        public string RenderToString()
        {
            var builder = new StringBuilder();
            Render(builder, 0);
            return builder.ToString();
        }
    }
}