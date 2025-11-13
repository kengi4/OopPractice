using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice1
{
    /// <summary>
    /// Represents a Fireball ability.
    /// </summary>
    public class Fireball : IAbility
    {
        /// <inheritdoc/>
        public string Name { get; } = "Fireball";

        /// <summary>
        /// Initializes a new instance of the <see cref="Fireball"/> class.
        /// </summary>
        public Fireball()
        {
        }

        /// <inheritdoc/>
        public void Use(Character caster, Character target)
        {
            int damage = 30;
            Console.WriteLine($"{caster.Name} hurls a fireball at {target.Name}!");

            target.TakeDamage(damage);
        }
    }
}
