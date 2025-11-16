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

        public override void Render(StringBuilder builder, int indentation, bool showIds)
        {
            string idPrefix = showIds ? $"[{Id.ToString().Substring(0, 8)}] " : "";
            builder.AppendLine($"{idPrefix}{Name}");

            base.Render(builder, indentation, showIds);
        }

        public string RenderToString(bool showIds = false)
        {
            var builder = new StringBuilder();
            Render(builder, 0, showIds);
            return builder.ToString();
        }
    }
}