using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// A specific visitor that implements the rendering logic into plain text.
    /// </summary>
    public class PlainTextVisitor : IVisitor
    {
        private readonly StringBuilder _builder = new StringBuilder();
        private int _indentation = 0;
        private readonly bool _showIds;

        public PlainTextVisitor(bool showIds = false)
        {
            _showIds = showIds;
        }

        public string GetResult() => _builder.ToString();

        public void Visit(Root root)
        {
            AppendText(root.Name, root.Id);
            _indentation++;
        }

        public void Visit(Header header)
        {
            AppendText(header.Name, header.Id);
            _indentation++;
        }

        public void Visit(Paragraph paragraph)
        {
            string content = paragraph.Content ?? string.Empty;

            AppendText(content, paragraph.Id);
        }

        private void AppendText(string text, Guid id)
        {
            string indent = new string('\t', _indentation);
            string idPrefix = _showIds ? $"[{id.ToString().Substring(0, 8)}] " : "";
            _builder.AppendLine($"{indent}{idPrefix}{text}");
        }
    }
}