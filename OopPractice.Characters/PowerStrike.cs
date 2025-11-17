using OopPractice.Display;

namespace OopPractice.Characters
{
    /// <summary>
    /// Represents a PowerStrike ability.
    /// </summary>
    public class PowerStrike : IAbility
    {
        /// <inheritdoc/>
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; } = "Power Strike";

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerStrike"/> class.
        /// </summary>
        public PowerStrike()
        {
        }

        /// <inheritdoc/>
        public void Use(Character caster, Character target, IDisplayer displayer)
        {
            int damage = (int)(caster.AttackPower * 1.25);
            displayer.Display($"{caster.Name} performs a Power Strike on {target.Name}!");
            target.TakeDamage(damage);
        }
    }
}
