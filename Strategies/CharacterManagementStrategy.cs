using OopPractice.Characters;
using OopPractice.Data;
using OopPractice.Display;

namespace OopPractice1.Strategies
{
    public class CharacterManagementStrategy : ICommandStrategy
    {
        private readonly List<Character> _characters = new();
        private readonly IRenderer<Character> _charRenderer;

        private readonly Repository _repository;
        private readonly IDisplayer _displayer;

        public CharacterManagementStrategy(IRenderer<Character> charRenderer, IDisplayer displayer)
        {
            _charRenderer = charRenderer;
            _displayer = displayer;
            _repository = new Repository(displayer);

            LoadData();
        }

        public void RegisterCommands(CliManager manager)
        {
            manager.RegisterCommand("ls-chars", ListCharacters);
            manager.RegisterCommand("create-char", CreateCharacter);
            manager.RegisterCommand("info", ShowInfo);
            manager.RegisterCommand("save", SaveData);
        }

        private void ListCharacters(string[] args)
        {
            Console.WriteLine("\n--- Character Roster ---");
            if (!_characters.Any())
            {
                Console.WriteLine("No characters found. Use 'create-char' to add one.");
                return;
            }

            foreach (var c in _characters)
            {
                Console.WriteLine($"- {c.Name} [{c.GetType().Name}] (HP: {c.Health})");
            }
            Console.WriteLine();
        }

        private void ShowInfo(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: info <name>");
                return;
            }

            var character = _characters.FirstOrDefault(c => c.Name.Equals(args[0], StringComparison.OrdinalIgnoreCase));
            if (character != null)
            {
                _charRenderer.Render(character);
            }
            else
            {
                Console.WriteLine("Character not found.");
            }
        }

        private void CreateCharacter(string[] args)
        {
            Console.WriteLine("\n--- Character Creation Wizard ---");

            string name = Prompt("Enter character name:");
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Creation cancelled: Name cannot be empty.");
                return;
            }

            string type = Prompt("Choose class (Warrior/Mage):").ToLower();

            Character newChar;
            try
            {
                switch (type)
                {
                    case "warrior":
                        newChar = new Warrior(name, _displayer);
                        break;
                    case "mage":
                        newChar = new Mage(name, _displayer);
                        break;
                    default:
                        newChar = new Character(name, 100, 5, 10, _displayer);
                        Console.WriteLine("Unknown class, creating generic Peasant.");
                        break;
                }

                _characters.Add(newChar);
                Console.WriteLine($"(+) Character '{newChar.Name}' created successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating character: {ex.Message}");
            }
        }

        private void SaveData(string[] args)
        {
            _repository.SaveGame(_characters);
            Console.WriteLine("Game state saved to JSON.");
        }

        private void LoadData()
        {
            var data = _repository.LoadGame();
            if (data.Count > 0)
            {
                _characters.Clear();
                foreach (var dto in data)
                {
                    Character c = dto.Type == "Mage"
                        ? new Mage(dto.Name, _displayer)
                        : new Warrior(dto.Name, _displayer);

                    c.RestoreState(dto.Health, dto.Armor, dto.AttackPower);
                    _characters.Add(c);
                }
                Console.WriteLine($"[System] Loaded {data.Count} characters from save file.");
            }
        }

        private string Prompt(string text)
        {
            Console.Write($"{text} ");
            return Console.ReadLine() ?? string.Empty;
        }
    }
}