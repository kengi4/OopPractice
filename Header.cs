using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice1
{
    /// <summary>
    /// Represents a header element.
    /// </summary>
    public class Header : TextElement
    {
        private readonly string _title;
        private readonly int _level;

        /// <summary>
        /// Initializes a new instance of the <see cref="Header"/> class.
        /// </summary>
        /// <param name="title">The title text of the header.</param>
        /// <param name="level">The level of the header (e.g., 1 for H1, 2 for H2).</param>
        public Header(string title, int level)
        {
            _title = title;
            _level = level;
        }

        /// <inheritdoc/>
        public override string Render()
        {
            if (_level == 1)
            {
                return $"== {_title.ToUpper()} ==";
            }
            else if (_level == 2)
            {
                return $"--- {_title} ---";
            }
            else
            {
                return $"# {_title}";
            }
        }

        /// <inheritdoc/>
        public override string? GetContentHeader()
        {
            if (_level == 1)
            {
                return $"- {_title}";
            }
            else if (_level == 2)
            {
                return $" - {_title}";
            }
            else
            {
                return $"   - {_title}";
            }
        }
    }
}
