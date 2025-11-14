using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Represents a paragraph (a leaf node).
    /// </summary>
    public class Paragraph : Leaf
    {
        public Paragraph(string? content) : base(content) { }

        public override void Render(StringBuilder builder, int indentation)
        {
            string indent = new string('\t', indentation);
            builder.AppendLine($"{indent}{_content ?? string.Empty}");
        }
    }
}