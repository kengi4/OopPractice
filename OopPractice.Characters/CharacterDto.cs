using System.Text;
using OopPractice.Display;
using System.Linq;

namespace OopPractice.Characters
{
    public class CharacterDto : IDisplayable
    {
        private readonly string _name;
        private readonly int _health;
        private readonly int _armor;
        private readonly int _attackPower;
        private readonly List<string> _items;
        private readonly List<string> _abilities;

        public CharacterDto(Character character)
        {
            _name = character.Name;
            _health = character.Health;
            _armor = character.Armor;
            _attackPower = character.AttackPower;

            _items = character.EquippedItems.Select(i => i.Name).ToList();
            _abilities = character.Abilities.Select(a => a.Name).ToList();
        }

        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"--- Character Stats: {_name} ---");
            sb.AppendLine($"HP: {_health} | Armor: {_armor} | AP: {_attackPower}");

            if (_items.Any())
            {
                sb.AppendLine("Items: " + string.Join(", ", _items));
            }

            if (_abilities.Any())
            {
                sb.AppendLine("Abilities: " + string.Join(", ", _abilities));
            }

            return sb.ToString();
        }
    }
}