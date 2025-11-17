using System.Text.Json;
using OopPractice.Display;

namespace OopPractice.Data
{
    public class Repository
    {
        private readonly string _filePath = "gamestate.json";
        private readonly IDisplayer _displayer;

        public Repository(IDisplayer displayer)
        {
            _displayer = displayer;
        }

        public void SaveGame(List<Character> characters)
        {
            var state = new GameState { SaveName = "AutoSave" };

            foreach (var charObj in characters)
            {
                var data = new CharacterData
                {
                    Name = charObj.Name,
                    Type = charObj.GetType().Name,
                    Health = charObj.Health,
                    Armor = charObj.Armor,
                    AttackPower = charObj.AttackPower,
                    ItemNames = charObj.EquippedItems.Select(i => i.Name).ToList(),
                    AbilityNames = charObj.Abilities.Select(a => a.Name).ToList()
                };
                state.Characters.Add(data);
            }

            string jsonString = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonString);
            _displayer.Display($"Game saved to {_filePath}");
        }

        public List<CharacterData> LoadGame()
        {
            if (!File.Exists(_filePath))
            {
                _displayer.Display("No save file found.");
                return new List<CharacterData>();
            }

            string jsonString = File.ReadAllText(_filePath);
            var state = JsonSerializer.Deserialize<GameState>(jsonString);

            _displayer.Display($"Game loaded from {_filePath} (Last modified: {state?.LastModified})");
            return state?.Characters ?? new List<CharacterData>();
        }
    }
}