using System.Text.RegularExpressions;

namespace OopPractice1
{
    /// <summary>
    /// Manages the registration and execution of CLI commands.
    /// Follows Open-Closed Principle by allowing new commands to be added
    /// without modifying this class.
    /// </summary>
    public class CliManager
    {
        private readonly Dictionary<string, Action<string[]>> _commands = new();

        /// <summary>
        /// Registers a new command.
        /// </summary>
        /// <param name="commandName">The keyword to invoke the command.</param>
        /// <param name="action">The function to execute.</param>
        public void RegisterCommand(string commandName, Action<string[]> action)
        {
            _commands[commandName.ToLower()] = action;
        }

        /// <summary>
        /// Starts the main Read-Evaluate-Print Loop (REPL).
        /// Continuously listens for user input.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("CLI manager started. Type 'exit' to quit.");
            while (true)
            {
                Console.Write("> ");
                string? input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input.ToLower() == "exit") break;

                ProcessInput(input);
            }
        }

        /// <summary>
        /// Parses the raw input string and executes the corresponding command.
        /// </summary>
        /// <param name="input">The raw string from the user.</param>
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
                Console.WriteLine($"Unknown command: '{commandName}'");
                Console.ResetColor();
            }
        }
    }
}