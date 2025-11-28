using OopPractice.Display;

namespace OopPractice.Characters
{
    public class DefaultAttackStrategy : IAttackStrategy
    {
        public void ExecuteAttack(Character attacker, Character target, IDisplayer displayer)
        {
            displayer.Display($"{attacker.Name} attacks {target.Name}!");
            target.TakeDamage(attacker.AttackPower);
        }
    }

    public class MightyAxeStrategy : IAttackStrategy
    {
        public void ExecuteAttack(Character attacker, Character target, IDisplayer displayer)
        {
            displayer.Display($"{attacker.Name} swings their mighty axe with fury!");

            int damage = (int)(attacker.AttackPower * 1.1);
            target.TakeDamage(damage);
        }
    }
}