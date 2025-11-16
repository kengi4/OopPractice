using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents a header (a container node).
    /// </summary>
    public class Header : Container
    {
        public Header(string name, Container? parent) : base(name, parent) { }

        public override void Render(StringBuilder builder, int indentation, bool showIds)
        {
            string indent = new string('\t', indentation);
            string idPrefix = showIds ? $"[{Id.ToString().Substring(0, 8)}] " : "";
            builder.AppendLine($"{indent}{idPrefix}{Name}");

            base.Render(builder, indentation, showIds);
        }
    }
}