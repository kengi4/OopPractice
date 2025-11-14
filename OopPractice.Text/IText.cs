using System.Text;

namespace OopPractice.Text
{
    /// <summary>
    /// Base interface for all text components.
    /// Provides a contract for rendering.
    /// </summary>
    public interface IText
    {
        /// <summary>
        /// Renders the component into a string.
        /// </summary>
        /// <param name="builder">The StringBuilder to append to.</param>
        /// <param name="indentation">The current indentation level.</param>
        void Render(StringBuilder builder, int indentation);
    }
}