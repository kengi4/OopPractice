using OopPractice.Display;

namespace OopPractice.Characters
{
    /// <summary>
    /// Represents a Fireball ability.
    /// </summary>
    public class Fireball : IAbility
    {
        /// <inheritdoc/>
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; } = "Fireball";

        /// <summary>
        /// Initializes a new instance of the <see cref="Fireball"/> class.
        /// </summary>
        public Fireball()
        {
        }

        /// <inheritdoc/>
        public void Use(Character caster, Character target, IDisplayer displayer)
        {
            int damage = 30;
            displayer.Display($"{caster.Name} hurls a fireball at {target.Name}!");
            target.TakeDamage(damage);
        }
    }
}
