using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OopPractice1
{
    /// <summary>
    /// Represents a Warrior, a specialized type of <see cref="Character"/>.
    /// </summary>
    public class Warrior : Character
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Warrior"/> class.
        /// </summary>
        /// <param name="name">The warrior's name.</param>
        public Warrior(string name)
            : base(name, 150, 10, 15)
        {
            _abilities.Add(new PowerStrike());
        }

        /// <summary>
        /// Overrides the base attack to show a custom message.
        /// </summary>
        public override void Attack(Character target)
        {
            Console.WriteLine($"{Name} swings their mighty axe!");
            base.Attack(target);
        }
    }
}
