using OopPractice.Display;

namespace OopPractice.Characters
{
    /// <summary>
    /// Defines the contract for an ability that a character can use.
    /// </summary>
    public interface IAbility
    {
        /// <summary>
        /// Gets the name of the ability.
        /// </summary>
        string Name { get; } // (1) Властивість "тільки для читання"
        Guid Id { get; }

        /// <summary>
        /// Executes the ability.
        /// </summary>
        /// <param name="caster">The character using the ability.</param>
        /// <param name="target">The target of the ability.</param>
        void Use(Character caster, Character target, IDisplayer displayer);
    }
}