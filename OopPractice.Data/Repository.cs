using OopPractice.Characters;
using OopPractice.Display;
using System.Text.Json;

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
                var memento = charObj.SaveState();

                var data = new CharacterData
                {
                    Name = charObj.Name,
                    Type = charObj.GetType().Name,
                    Health = memento.Health,
                    Armor = memento.Armor,
                    AttackPower = memento.AttackPower,
                    ItemNames = memento.ItemNames,
                    AbilityNames = memento.AbilityNames
                };
                state.Characters.Add(data);
            }

            string jsonString = JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonString);
            _displayer.Display($"Game saved to {_filePath}");
        }

        public List<CharacterData> LoadGame()
        {
            if (!File.Exists(_filePath)) return new List<CharacterData>();
            string jsonString = File.ReadAllText(_filePath);
            var state = JsonSerializer.Deserialize<GameState>(jsonString);
            _displayer.Display($"Game loaded...");

            return state?.Characters ?? new List<CharacterData>();
        }
    }
}