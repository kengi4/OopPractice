using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice1
{
    /// <summary>
    /// Represents a simple paragraph of text.
    /// </summary>
    public class Paragraph : TextElement
    {
        /// <summary>
        /// Stores the content of the paragraph.
        /// </summary>
        private readonly string _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="Paragraph"/> class.
        /// </summary>
        /// <param name="text">The content of the paragraph.</param>
        public Paragraph(string text)
        {
            _text = text;
        }

        /// <inheritdoc/> 
        public override string Render()
        {
            return _text;
        }
    }
}
