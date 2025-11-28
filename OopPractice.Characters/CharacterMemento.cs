namespace OopPractice.Characters
{
    /// <summary>
    /// Stores the internal state of the Character object.
    /// Object is immutable.
    /// </summary>
    public class CharacterMemento
    {
        public int Health { get; }
        public int Armor { get; }
        public int AttackPower { get; }
        public List<string> AbilityNames { get; }
        public List<string> ItemNames { get; }

        public CharacterMemento(int health, int armor, int ap, List<string> abilities, List<string> items)
        {
            Health = health;
            Armor = armor;
            AttackPower = ap;
            AbilityNames = new List<string>(abilities);
            ItemNames = new List<string>(items);
        }
    }
}