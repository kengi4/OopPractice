using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice.Characters
{
    /// <summary>
    /// Defines the contract for an item that a character can equip.
    /// </summary>
    public interface IItem
    {
        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Applies the item's effects when equipped.
        /// </summary>
        /// <param name="target">The character equipping the item.</param>
        void Equip(Character target);

        /// <summary>
        /// Removes the item's effects when unequipped.
        /// </summary>
        /// <param name="target">The character unequipping the item.</param>
        void Unequip(Character target);
    }
}
