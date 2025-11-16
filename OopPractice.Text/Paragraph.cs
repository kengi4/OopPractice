using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents a paragraph (a leaf node).
    /// </summary>
    public class Paragraph : Leaf
    {
        public Paragraph(string? content) : base(content) { }

        public override void Render(StringBuilder builder, int indentation, bool showIds)
        {
            string indent = new string('\t', indentation);
            string idPrefix = showIds ? $"[{Id.ToString().Substring(0, 8)}] " : "";
            builder.AppendLine($"{indent}{idPrefix}{_content ?? string.Empty}");
        }
    }
}