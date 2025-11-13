using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice.Characters
{
    /// <summary>
    /// Represents a Sword item that grants bonus attack power.
    /// </summary>
    public class Sword : IItem
    {
        private readonly int _attackBonus = 10;


        /// <inheritdoc/>
        public string Name { get; } = "Iron Sword";

        /// <summary>
        /// Applies the item's effects when equipped.
        /// /// </summary>
        public void Equip(Character target, ILogger logger)
        {
            target.ApplyStatModifier(attackMod: _attackBonus, armorMod: 0);
            logger.Log($"{target.Name}'s Attack Power increased by {_attackBonus}!");
        }

        public void Unequip(Character target, ILogger logger)
        {
            target.RemoveStatModifier(attackMod: _attackBonus, armorMod: 0);
            logger.Log($"{target.Name}'s Attack Power returned to normal.");
        }
    }
}
