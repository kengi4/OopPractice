using OopPractice.Display;

namespace OopPractice.Characters
{
    /// <summary>
    /// Represents a Sword item that grants bonus attack power.
    /// </summary>
    public class Sword : IItem
    {
        private readonly int _attackBonus = 10;

        /// <inheritdoc/>
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; } = "Iron Sword";

        /// <summary>
        /// Applies the item's effects when equipped.
        /// /// </summary>
        public void Equip(Character target, IDisplayer displayer)
        {
            target.ApplyStatModifier(attackMod: _attackBonus, armorMod: 0);
            displayer.Display($"{target.Name}'s Attack Power increased by {_attackBonus}!");
        }

        public void Unequip(Character target, IDisplayer displayer)
        {
            target.RemoveStatModifier(attackMod: _attackBonus, armorMod: 0);
            displayer.Display($"{target.Name}'s Attack Power returned to normal.");
        }
    }
}
