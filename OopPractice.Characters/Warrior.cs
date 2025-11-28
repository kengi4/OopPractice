using OopPractice.Display;

namespace OopPractice.Characters
{
    public class Warrior : Character
    {
        public Warrior(string name, IDisplayer displayer)
            : base(name, 150, 10, 15, displayer)
        {
            _abilities.Add(new PowerStrike());

            SetAttackStrategy(new MightyAxeStrategy());
        }
    }
}