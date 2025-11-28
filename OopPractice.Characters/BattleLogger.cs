using OopPractice.Display;

namespace OopPractice.Characters
{
    /// <summary>
    /// Specific observer.
    /// Follows the events of the characters and displays important messages.
    /// </summary>
    public class BattleLogger : ICharacterObserver
    {
        private readonly IDisplayer _displayer;

        public BattleLogger(IDisplayer displayer)
        {
            _displayer = displayer;
        }

        public void OnHealthChanged(Character character, int currentHealth, int damageTaken)
        {
            if (damageTaken > 0)
            {
                _displayer.Display($"[OBSERVER LOG] {character.Name} lost {damageTaken} HP! (Health: {currentHealth})");
            }
        }

        public void OnCharacterDied(Character character)
        {
            _displayer.Display($"[OBSERVER LOG] ☠️ {character.Name} has fallen in battle! ☠️");
        }
    }
}