namespace OopPractice.Data
{
    public class CharacterData
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Health { get; set; }
        public int Armor { get; set; }
        public int AttackPower { get; set; }
        public List<string> ItemNames { get; set; } = new();
        public List<string> AbilityNames { get; set; } = new();
    }

    public class GameState : SaveBase
    {
        public List<CharacterData> Characters { get; set; } = new();
    }
}