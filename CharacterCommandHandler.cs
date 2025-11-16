using OopPractice.Characters;

namespace OopPractice1
{
    /// <summary>
    /// Handles all logic for the 'Characters' CLI mode.
    /// </summary>
    public class CharacterCommandHandler
    {
        private readonly ILogger _logger;

        private readonly List<Character> _characters = new();
        private readonly List<IItem> _items = new();
        private readonly List<IAbility> _abilities = new();

        public CharacterCommandHandler(ILogger logger)
        {
            _logger = logger;
            SeedData();
        }

        public void RegisterCommands(CliManager manager)
        {
            manager.RegisterCommand("create", Create);
            manager.RegisterCommand("add", Add);
            manager.RegisterCommand("act", Act);
            manager.RegisterCommand("ls", ListAll);
        }

        private IItem? FindItem(string identifier)
        {
            IItem? item = _items.FirstOrDefault(i =>
                i.Id.ToString().StartsWith(identifier) ||
                i.Name.Equals(identifier, StringComparison.OrdinalIgnoreCase));

            if (item == null)
            {
                _logger.Log($"Error: Item '{identifier}' not found.");
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
                _logger.Log($"Error: Ability '{identifier}' not found.");
            }
            return ability;
        }

        private void Create(string[] args)
        {
            if (args.Length == 0)
            {
                _logger.Log("Usage: create <char|item|ability>");
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
                        _logger.Log($"Unknown type to create: {type}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                _logger.Log($"Failed to create: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void CreateCharacter()
        {
            _logger.Log("--- Creating new Character ---");
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
                    newChar = new Warrior(name, _logger);
                    break;
                case "mage":
                    newChar = new Mage(name, _logger);
                    break;
                default:
                    throw new Exception($"Unknown character type: {type}");
            }

            _characters.Add(newChar);
            _logger.Log($"Created {type} '{name}' with ID: {newChar.Id.ToString().Substring(0, 8)}");
        }

        private void CreateItem()
        {
            _logger.Log("--- Creating new Item ---");
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
            _logger.Log($"Created {type} '{newItem.Name}' with ID: {newItem.Id.ToString().Substring(0, 8)}");
        }

        private void CreateAbility()
        {
            _logger.Log("--- Creating new Ability ---");
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
            _logger.Log($"Created {type} '{newAbility.Name}' with ID: {newAbility.Id.ToString().Substring(0, 8)}");
        }

        private void Add(string[] args)
        {
            var (positional, options) = ParseArgs(args);

            if (!options.ContainsKey("--char_id") || !options.ContainsKey("--id"))
            {
                _logger.Log("Usage: add --char_id <char_id_or_name> --id <item_or_ability_id_or_name>");
                return;
            }

            string charIdentifier = options["--char_id"];
            string entityIdentifier = options["--id"];

            Character? character = _characters.FirstOrDefault(c =>
                c.Id.ToString().StartsWith(charIdentifier) ||
                c.Name.Equals(charIdentifier, StringComparison.OrdinalIgnoreCase));

            if (character == null)
            {
                _logger.Log($"Error: Character '{charIdentifier}' not found.");
                return;
            }

            IItem? item = _items.FirstOrDefault(i =>
                i.Id.ToString().StartsWith(entityIdentifier) ||
                i.Name.Equals(entityIdentifier, StringComparison.OrdinalIgnoreCase));

            if (item != null)
            {
                character.EquipItem(item);
                _logger.Log($"Equipped '{item.Name}' on '{character.Name}'.");
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

            _logger.Log($"Error: Item or Ability '{entityIdentifier}' not found.");
        }

        private void Act(string[] args)
        {
            var (positional, options) = ParseArgs(args);

            if (positional.Count < 1)
            {
                _logger.Log("Usage: act <action_type> [args...]");
                _logger.Log("Available actions: attack, heal, ability");
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
                        _logger.Log("Usage: act attack <actor_name_or_id> <target_name_or_id>");
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
                        _logger.Log("Usage: act ability <actor_name_or_id> <target_name_or_id> --id <ability_name>");
                        return;
                    }
                    if (!options.ContainsKey("--id"))
                    {
                        _logger.Log("Error: Missing --id <ability_name> for 'ability' action.");
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
                        _logger.Log("Usage: act heal <actor_name_or_id>");
                        return;
                    }

                    actor = FindCharacter(positional[1]);
                    if (actor != null)
                    {
                        actor.Heal(20);
                    }
                    break;

                default:
                    _logger.Log($"Error: Unknown action type '{actionType}'.");
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
                _logger.Log($"Error: Character '{identifier}' not found.");
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
                _logger.Log("Usage: ls <char|item|ability> [--id <id/name>]");
                return;
            }

            string type = positional[0].ToLower();

            if (options.ContainsKey("--id"))
            {
                string idOrName = options["--id"];
                _logger.Log($"--- Showing details for {type} '{idOrName}' ---");

                switch (type)
                {
                    case "char":
                        Character? c = FindCharacter(idOrName);
                        if (c != null)
                        {
                            _logger.Log($"Name: {c.Name} [{c.Id.ToString().Substring(0, 8)}]");
                            _logger.Log($"HP: {c.Health}, AP: {c.AttackPower}, Armor: {c.Armor}");

                            _logger.Log("Equipped Items:");
                            foreach (var item in c.EquippedItems)
                            {
                                _logger.Log($"  - {item.Name} [{item.Id.ToString().Substring(0, 8)}]");
                            }
                            if (!c.EquippedItems.Any()) _logger.Log("  (None)");

                            _logger.Log("Abilities:");
                            foreach (var ability in c.Abilities)
                            {
                                _logger.Log($"  - {ability.Name} [{ability.Id.ToString().Substring(0, 8)}]");
                            }
                            if (!c.Abilities.Any()) _logger.Log("  (None)");
                        }
                        break;

                    case "item":
                        IItem? i = FindItem(idOrName);
                        if (i != null)
                        {
                            _logger.Log($"Name: {i.Name} [{i.Id.ToString().Substring(0, 8)}]");
                        }
                        break;

                    case "ability":
                        IAbility? a = FindAbility(idOrName);
                        if (a != null)
                        {
                            _logger.Log($"Name: {a.Name} [{a.Id.ToString().Substring(0, 8)}]");
                        }
                        break;

                    default:
                        _logger.Log($"Unknown type: {type}");
                        break;
                }
            }
            else
            {
                _logger.Log($"--- Listing all {type} ---");

                if (type == "char")
                {
                    foreach (var c in _characters)
                    {
                        _logger.Log($"[{c.Id.ToString().Substring(0, 8)}] {c.Name} (HP: {c.Health}, AP: {c.AttackPower})");
                    }
                }
                else if (type == "item")
                {
                    foreach (var i in _items)
                    {
                        _logger.Log($"[{i.Id.ToString().Substring(0, 8)}] {i.Name}");
                    }
                }
                else if (type == "ability")
                {
                    foreach (var a in _abilities)
                    {
                        _logger.Log($"[{a.Id.ToString().Substring(0, 8)}] {a.Name}");
                    }
                }
                else
                {
                    _logger.Log($"Unknown type: {type}");
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
            _characters.Add(new Warrior("Aragorn", _logger));
            _characters.Add(new Mage("Gandalf", _logger));

            _items.Add(new Sword());

            _abilities.Add(new Fireball());
            _abilities.Add(new PowerStrike());
        }
    }

}