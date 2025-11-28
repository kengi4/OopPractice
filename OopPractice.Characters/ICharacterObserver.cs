namespace OopPractice.Characters
{
    /// <summary>
    /// Interface for objects that subscribe to character events.
    /// </summary>
    public interface ICharacterObserver
    {
        void OnHealthChanged(Character character, int currentHealth, int damageTaken);
        void OnCharacterDied(Character character);
    }
}