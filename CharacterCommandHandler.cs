using OopPractice.Characters;
using OopPractice.Data;
using OopPractice.Display;

namespace OopPractice1
{
    /// <summary>
    /// Handles all logic for the 'Characters' CLI mode.
    /// </summary>
    public class CharacterCommandHandler
    {
        private readonly IDisplayer _displayer;
        private readonly Repository _repository;

        private readonly List<Character> _characters = new();
        private readonly List<IItem> _items = new();
        private readonly List<IAbility> _abilities = new();

        private readonly OopPractice.Infra.GenshinApiClient _apiClient;

        public CharacterCommandHandler(IDisplayer displayer)
        {
            _displayer = displayer;
            _repository = new Repository(displayer);
            _apiClient = new OopPractice.Infra.GenshinApiClient(displayer);
            SeedData();
        }

        public void RegisterCommands(CliManager manager)
        {
            manager.RegisterCommand("create", Create);
            manager.RegisterCommand("add", Add);
            manager.RegisterCommand("act", Act);
            manager.RegisterCommand("ls", ListAll);
            manager.RegisterCommand("save", Save);
            manager.RegisterCommand("load", Load);
            manager.RegisterCommand("api-list", ApiList);
            manager.RegisterCommand("api-create", ApiCreate);
        }

        private IItem? FindItem(string identifier)
        {
            IItem? item = _items.FirstOrDefault(i =>
                i.Id.ToString().StartsWith(identifier) ||
                i.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                _displayer.Display($"Error: Item '{identifier}' not found.");
            }
            return item;
        }

        private IAbility? FindAbility(string identifier)
        {
            IAbility? ability = _abilities.FirstOrDefault(a =>
                a.Id.ToString().StartsWith(identifier) ||
                a.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase));

            if (ability == null)
            {
                _displayer.Display($"Error: Ability '{identifier}' not found.");
            }
            return ability;
        }

        private async void ApiCreate(string[] args)
        {
            if (args.Length == 0)
            {
                _displayer.Display("Usage: api-create <character_name>");
                return;
            }

            string name = args[0];
            _displayer.Display($"Fetching '{name}' data from API...");

            try
            {
                Character newChar = await _apiClient.GetCharacterAsync(name);

                _characters.Add(newChar);
                _displayer.Display($"Successfully created character '{newChar.Name}' from API!");
                _displayer.Display($"Stats -> HP: {newChar.Health}, AP: {newChar.AttackPower}, Armor: {newChar.Armor}");
            }
            catch (Exception ex)
            {
                _displayer.Display($"Error: Could not create character. {ex.Message}");
            }
        }

        private async void ApiList(string[] args)
        {
            _displayer.Display("Fetching characters list from API...");
            try
            {
                var list = await _apiClient.GetCharactersListAsync();
                _displayer.Display($"Found {list.Count} characters:");
                foreach (var name in list.Take(10))
                {
                    _displayer.Display($" - {name}");
                }
                _displayer.Display("... (and more)");
            }
            catch (Exception ex)
            {
                _displayer.Display($"API Error: {ex.Message}");
            }
        }



        private void Create(string[] args)
        {
            if (args.Length == 0)
            {
                _displayer.Display("Usage: create <char|item|ability>");
                return;
            }

            string type = args[0].ToLower();
            try
            {
                switch (type)
                {
                    case "char":
                        CreateCharacter();
                        break;
                    case "item":
                        CreateItem();
                        break;
                    case "ability":
                        CreateAbility();
                        break;
                    default:
                        _displayer.Display($"Unknown type to create: {type}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                _displayer.Display($"Failed to create: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void CreateCharacter()
        {
            _displayer.Display("--- Creating new Character ---");
            string name = Prompt("Enter name:");
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new Exception("Name cannot be empty.");
            }

            string type = Prompt("Enter type (Warrior/Mage):").ToLower();
            Character newChar;

            switch (type)
            {
                case "warrior":
                    newChar = new Warrior(name, _displayer);
                    break;
                case "mage":
                    newChar = new Mage(name, _displayer);
                    break;
                default:
                    throw new Exception($"Unknown character type: {type}");
            }

            _characters.Add(newChar);
            _displayer.Display($"Created {type} '{name}' with ID: {newChar.Id.ToString().Substring(0, 8)}");
        }

        private void CreateItem()
        {
            _displayer.Display("--- Creating new Item ---");
            string type = Prompt("Enter type (Sword):").ToLower();
            IItem newItem;

            switch (type)
            {
                case "sword":
                    newItem = new Sword();
                    break;
                default:
                    throw new Exception($"Unknown item type: {type}");
            }

            _items.Add(newItem);
            _displayer.Display($"Created {type} '{newItem.Name}' with ID: {newItem.Id.ToString().Substring(0, 8)}");
        }

        private void CreateAbility()
        {
            _displayer.Display("--- Creating new Ability ---");
            string type = Prompt("Enter type (Fireball/PowerStrike):").ToLower();
            IAbility newAbility;

            switch (type)
            {
                case "fireball":
                    newAbility = new Fireball();
                    break;
                case "powerstrike":
                    newAbility = new PowerStrike();
                    break;
                default:
                    throw new Exception($"Unknown ability type: {type}");
            }

            _abilities.Add(newAbility);
            _displayer.Display($"Created {type} '{newAbility.Name}' with ID: {newAbility.Id.ToString().Substring(0, 8)}");
        }

        private void Add(string[] args)
        {
            var (positional, options) = ParseArgs(args);

            if (!options.ContainsKey("--char_id") || !options.ContainsKey("--id"))
            {
                _displayer.Display("Usage: add --char_id <char_id_or_name> --id <item_or_ability_id_or_name>");
                return;
            }

            string charIdentifier = options["--char_id"];
            string entityIdentifier = options["--id"];

            Character? character = _characters.FirstOrDefault(c =>
                c.Id.ToString().StartsWith(charIdentifier) ||
                c.Name.Equals(charIdentifier, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                _displayer.Display($"Error: Character '{charIdentifier}' not found.");
                return;
            }

            IItem? item = _items.FirstOrDefault(i =>
                i.Id.ToString().StartsWith(entityIdentifier) ||
                i.Name.Equals(entityIdentifier, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                character.EquipItem(item);
                _displayer.Display($"Equipped '{item.Name}' on '{character.Name}'.");
                return;
            }

            IAbility? ability = _abilities.FirstOrDefault(a =>
                a.Id.ToString().StartsWith(entityIdentifier) ||
                a.Name.Equals(entityIdentifier, StringComparison.OrdinalIgnoreCase));

            if (ability != null)
            {
                character.AddAbility(ability);
                return;
            }

            _displayer.Display($"Error: Item or Ability '{entityIdentifier}' not found.");
        }

        private void Act(string[] args)
        {
            var (positional, options) = ParseArgs(args);

            if (positional.Count < 1)
            {
                _displayer.Display("Usage: act <action_type> [args...]");
                _displayer.Display("Available actions: attack, heal, ability");
                return;
            }

            string actionType = positional[0].ToLower();
            Character? actor;
            Character? target;

            switch (actionType)
            {
                case "attack":
                    if (positional.Count < 3)
                    {
                        _displayer.Display("Usage: act attack <actor_name_or_id> <target_name_or_id>");
                        return;
                    }
                    actor = FindCharacter(positional[1]);
                    target = FindCharacter(positional[2]);
                    if (actor != null && target != null)
                    {
                        actor.Attack(target);
                    }
                    break;

                case "ability":
                    if (positional.Count < 3)
                    {
                        _displayer.Display("Usage: act ability <actor_name_or_id> <target_name_or_id> --id <ability_name>");
                        return;
                    }
                    if (!options.ContainsKey("--id"))
                    {
                        _displayer.Display("Error: Missing --id <ability_name> for 'ability' action.");
                        return;
                    }

                    actor = FindCharacter(positional[1]);
                    target = FindCharacter(positional[2]);
                    if (actor != null && target != null)
                    {
                        string abilityIdentifier = options["--id"];
                        actor.UseAbility(abilityIdentifier, target);
                    }
                    break;

                case "heal":
                    if (positional.Count < 2)
                    {
                        _displayer.Display("Usage: act heal <actor_name_or_id>");
                        return;
                    }

                    actor = FindCharacter(positional[1]);
                    if (actor != null)
                    {
                        actor.Heal(20);
                    }
                    break;

                default:
                    _displayer.Display($"Error: Unknown action type '{actionType}'.");
                    break;
            }
        }

        /// <summary>
        /// Finds a character in the list by their ID (prefix) or full name.
        /// Logs an error if not found.
        /// </summary>
        private Character? FindCharacter(string identifier)
        {
            Character? character = _characters.FirstOrDefault(c =>
                c.Id.ToString().StartsWith(identifier) ||
                c.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                _displayer.Display($"Error: Character '{identifier}' not found.");
            }
            return character;
        }

        /// <summary>
        /// Parses an argument array into positional arguments and key-value options.
        /// Example: "arg1 --id 123" -> positional["arg1"], options["--id"] = "123"
        /// </summary>
        /// <returns>A tuple containing a list of positional args and a dictionary of options.</returns>
        private (List<string> positionalArgs, Dictionary<string, string> options) ParseArgs(string[] args)
        {
            var positionalArgs = new List<string>();
            var options = new Dictionary<string, string>();
            string? currentOption = null;

            foreach (var arg in args)
            {
                if (arg.StartsWith("--") || arg.StartsWith("-"))
                {
                    if (currentOption != null)
                    {
                        options[currentOption] = "true";
                    }
                    currentOption = arg;
                }
                else if (currentOption != null)
                {
                    options[currentOption] = arg;
                    currentOption = null;
                }
                else
                {
                    positionalArgs.Add(arg);
                }
            }

            if (currentOption != null)
            {
                options[currentOption] = "true";
            }

            return (positionalArgs, options);
        }

        private void ListAll(string[] args)
        {
            var (positional, options) = ParseArgs(args);

            if (positional.Count == 0)
            {
                _displayer.Display("Usage: ls <char|item|ability> [--id <id/name>]");
                return;
            }

            string type = positional[0].ToLower();

            if (options.ContainsKey("--id"))
            {
                string idOrName = options["--id"];
                _displayer.Display($"--- Showing details for {type} '{idOrName}' ---");

                switch (type)
                {
                    case "char":
                        Character? c = FindCharacter(idOrName);
                        if (c != null)
                        {
                            _displayer.Display($"Name: {c.Name} [{c.Id.ToString().Substring(0, 8)}]");
                            _displayer.Display($"HP: {c.Health}, AP: {c.AttackPower}, Armor: {c.Armor}");

                            _displayer.Display("Equipped Items:");
                            foreach (var item in c.EquippedItems)
                            {
                                _displayer.Display($"  - {item.Name} [{item.Id.ToString().Substring(0, 8)}]");
                            }
                            if (!c.EquippedItems.Any()) _displayer.Display("  (None)");

                            _displayer.Display("Abilities:");
                            foreach (var ability in c.Abilities)
                            {
                                _displayer.Display($"  - {ability.Name} [{ability.Id.ToString().Substring(0, 8)}]");
                            }
                            if (!c.Abilities.Any()) _displayer.Display("  (None)");
                        }
                        break;

                    case "item":
                        IItem? i = FindItem(idOrName);
                        if (i != null)
                        {
                            _displayer.Display($"Name: {i.Name} [{i.Id.ToString().Substring(0, 8)}]");
                        }
                        break;

                    case "ability":
                        IAbility? a = FindAbility(idOrName);
                        if (a != null)
                        {
                            _displayer.Display($"Name: {a.Name} [{a.Id.ToString().Substring(0, 8)}]");
                        }
                        break;

                    default:
                        _displayer.Display($"Unknown type: {type}");
                        break;
                }
            }
            else
            {
                _displayer.Display($"--- Listing all {type} ---");

                if (type == "char")
                {
                    foreach (var c in _characters)
                    {
                        _displayer.Display($"[{c.Id.ToString().Substring(0, 8)}] {c.Name} (HP: {c.Health}, AP: {c.AttackPower})");
                    }
                }
                else if (type == "item")
                {
                    foreach (var i in _items)
                    {
                        _displayer.Display($"[{i.Id.ToString().Substring(0, 8)}] {i.Name}");
                    }
                }
                else if (type == "ability")
                {
                    foreach (var a in _abilities)
                    {
                        _displayer.Display($"[{a.Id.ToString().Substring(0, 8)}] {a.Name}");
                    }
                }
                else
                {
                    _displayer.Display($"Unknown type: {type}");
                }
            }
        }

        private string Prompt(string message)
        {
            Console.Write(message + " ");
            return Console.ReadLine() ?? string.Empty;
        }

        private void SeedData()
        {
            _characters.Add(new Warrior("Aragorn", _displayer));
            _characters.Add(new Mage("Gandalf", _displayer));

            _items.Add(new Sword());

            _abilities.Add(new Fireball());
            _abilities.Add(new PowerStrike());
        }
        private void Save(string[] args)
        {
            _repository.SaveGame(_characters);
        }

        private void Load(string[] args)
        {
            var loadedData = _repository.LoadGame();
            if (loadedData.Count == 0) return;

            _characters.Clear();

            foreach (var data in loadedData)
            {
                Character character;

                if (data.Type == nameof(Warrior))
                {
                    character = new Warrior(data.Name, _displayer);
                }
                else if (data.Type == nameof(Mage))
                {
                    character = new Mage(data.Name, _displayer);
                }
                else
                {
                    character = new Character(data.Name, data.Health, data.Armor, data.AttackPower, _displayer);
                }

                character.RestoreState(data.Health, data.Armor, data.AttackPower);

                foreach (var itemName in data.ItemNames)
                {
                    var item = _items.FirstOrDefault(i => i.Name == itemName);
                    if (item != null) character.EquipItem(item);
                }

                foreach (var abilityName in data.AbilityNames)
                {
                    if (!character.Abilities.Any(a => a.Name == abilityName))
                    {
                        var ability = _abilities.FirstOrDefault(a => a.Name == abilityName);
                        if (ability != null) character.AddAbility(ability);
                    }
                }

                _characters.Add(character);
            }

            _displayer.Display("Characters state restored.");
        }
    }
}