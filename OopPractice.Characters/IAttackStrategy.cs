using OopPractice.Display;

namespace OopPractice.Characters
{
    /// <summary>
    /// Pattern: Strategy
    /// Defines a family of attack algorithms.
    /// </summary>
    public interface IAttackStrategy
    {
        void ExecuteAttack(Character attacker, Character target, IDisplayer displayer);
    }
}