using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice.Characters
{
    /// <summary>
    /// Represents a PowerStrike ability.
    /// </summary>
    public class PowerStrike : IAbility
    {
        /// <inheritdoc/>
        public string Name { get; } = "Power Strike";

        /// <summary>
        /// Initializes a new instance of the <see cref="PowerStrike"/> class.
        /// </summary>
        public PowerStrike()
        {
        }

        /// <inheritdoc/>
        public void Use(Character caster, Character target, ILogger logger)
        {
            int damage = (int)(caster.AttackPower * 1.25);
            logger.Log($"{caster.Name} performs a Power Strike on {target.Name}!");
            target.TakeDamage(damage);
        }
    }
}
