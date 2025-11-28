using OopPractice.Characters;
using OopPractice.FileManager;
using OopPractice.Display;

namespace OopPractice.Renderers
{
    public class CharacterConsoleRenderer : IRenderer<Character>
    {
        private readonly IConsoleDriver _driver;

        public CharacterConsoleRenderer(IConsoleDriver driver)
        {
            _driver = driver;
        }

        public void Render(Character character)
        {
            _driver.SetColor(ConsoleColor.Cyan);
            _driver.WriteAt(-1, -1, $"=== {character.Name} [{character.GetType().Name}] ===");

            _driver.ResetColor();
            Console.WriteLine($"HP: {character.Health} | Armor: {character.Armor} | AP: {character.AttackPower}");

            if (character.EquippedItems.Any())
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Items: " + string.Join(", ", character.EquippedItems.Select(i => i.Name)));
                Console.ResetColor();
            }
        }
    }
}