using OopPractice.Characters;
using OopPractice.Display;
using System.Text.Json;

namespace OopPractice.Data
{
    public class Repository
    {
        private readonly string _filePath;
        private readonly IDisplayer _displayer;

        public Repository(IDisplayer displayer)
        {
            _displayer = displayer;

            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string gameFolder = Path.Combine(appDataPath, "OopPracticeRPG");

            if (!Directory.Exists(gameFolder))
            {
                Directory.CreateDirectory(gameFolder);
            }

            _filePath = Path.Combine(gameFolder, "gamestate.json");
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
            _displayer.Display($"[System] Game saved to: {_filePath}");
        }

        public List<CharacterData> LoadGame()
        {
            if (!File.Exists(_filePath))
            {
                _displayer.Display("[System] No save file found.");
                return new List<CharacterData>();
            }

            try
            {
                string jsonString = File.ReadAllText(_filePath);
                var state = JsonSerializer.Deserialize<GameState>(jsonString);
                _displayer.Display($"[System] Game loaded successfully from: {_filePath}");
                return state?.Characters ?? new List<CharacterData>();
            }
            catch (Exception ex)
            {
                _displayer.Display($"[System] Error loading save file: {ex.Message}");
                return new List<CharacterData>();
            }
        }
    }
}