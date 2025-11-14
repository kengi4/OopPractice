using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents a header (a container node).
    /// </summary>
    public class Header : Container
    {
        public Header(string name, Container? parent) : base(name, parent) { }

        public override void Render(StringBuilder builder, int indentation)
        {
            string indent = new string('\t', indentation);
            builder.AppendLine($"{indent}{_name}");

            base.Render(builder, indentation);
        }
    }
}