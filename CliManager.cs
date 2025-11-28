using System.Text.RegularExpressions;

namespace OopPractice1
{
    public class CliManager
    {
        private readonly Dictionary<string, Action<string[]>> _commands = new();

        // 1. Додаємо конструктор, де реєструємо команду "help"
        public CliManager()
        {
            RegisterCommand("help", PrintHelp);
        }

        public void RegisterCommand(string commandName, Action<string[]> action)
        {
            _commands[commandName.ToLower()] = action;
        }

        public void UseStrategy(OopPractice1.Strategies.ICommandStrategy strategy)
        {
            strategy.RegisterCommands(this);
        }

        public void Run()
        {
            Console.WriteLine("CLI System Ready. Waiting for input...");
            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.ToLower() == "exit") break;

                ProcessInput(input);
            }
        }

        private void ProcessInput(string input)
        {
            var parts = Regex.Matches(input, @"[\""].+?[\""]|[^ ]+")
                .Cast<Match>()
                .Select(m => m.Value.Trim('"'))
                .ToArray();

            if (parts.Length == 0) return;

            string commandName = parts[0].ToLower();
            string[] args = parts.Skip(1).ToArray();

            if (_commands.ContainsKey(commandName))
            {
                try
                {
                    _commands[commandName](args);
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Error executing command '{commandName}': {ex.Message}");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Unknown command: '{commandName}'. Type 'help' to list commands.");
                Console.ResetColor();
            }
        }

        private void PrintHelp(string[] args)
        {
            Console.WriteLine("\n--- Available Commands ---");
            foreach (var command in _commands.Keys.OrderBy(k => k))
            {
                Console.WriteLine($" - {command}");
            }
            Console.WriteLine("--------------------------\n");
        }
    }
}