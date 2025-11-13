using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Executes the ability.
        /// </summary>
        /// <param name="caster">The character using the ability.</param>
        /// <param name="target">The target of the ability.</param>
        void Use(Character caster, Character target);
    }
}