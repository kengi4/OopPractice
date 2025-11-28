using OopPractice.Display;
using System.Linq;

namespace OopPractice.Characters
{
    /// <summary>
    /// Represents a base character in the game.
    /// Acts as the 'Context' for Strategy pattern and 'Subject' for Observer pattern.
    /// </summary>
    public class Character
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; private set; }
        public int Health { get; private set; }
        public int Armor { get; private set; }
        public int AttackPower { get; private set; }

        private IAttackStrategy _attackStrategy;

        public IEnumerable<IItem> EquippedItems => _equippedItems;
        public IEnumerable<IAbility> Abilities => _abilities;

        private readonly IDisplayer _displayer;

        protected List<IAbility> _abilities = new List<IAbility>();
        protected List<IItem> _equippedItems = new List<IItem>();

        private readonly List<ICharacterObserver> _observers = new List<ICharacterObserver>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Character"/> class.
        /// </summary>
        public Character(string name, int health, int armor, int attackPower, IDisplayer displayer)
        {
            Name = name;
            Health = health;
            Armor = armor;
            AttackPower = attackPower;
            _displayer = displayer;

            _attackStrategy = new DefaultAttackStrategy();
        }

        public void Subscribe(ICharacterObserver observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
        }

        public void Unsubscribe(ICharacterObserver observer)
        {
            _observers.Remove(observer);
        }

        protected void NotifyHealthChanged(int damageTaken)
        {
            foreach (var observer in _observers)
            {
                observer.OnHealthChanged(this, Health, damageTaken);
            }
        }

        protected void NotifyDied()
        {
            foreach (var observer in _observers)
            {
                observer.OnCharacterDied(this);
            }
        }

        /// <summary>
        /// Attacks another character using the current Strategy.
        /// </summary>
        public virtual void Attack(Character target)
        {
            // Pattern: Strategy Execution
            _attackStrategy.ExecuteAttack(this, target, _displayer);
        }

        public void SetAttackStrategy(IAttackStrategy strategy)
        {
            _attackStrategy = strategy;
        }

        /// <summary>
        /// Reduces health based on incoming damage, considering armor.
        /// </summary>
        public virtual void TakeDamage(int amount)
        {
            int damageTaken = Math.Max(0, amount - Armor);
            Health -= damageTaken;

            _displayer.Display($"{Name} takes {damageTaken} damage. Current health: {Health}");

            NotifyHealthChanged(damageTaken);

            if (Health <= 0)
            {
                _displayer.Display($"{Name} has been defeated!");
                NotifyDied();
            }
        }

        public void Heal(int amount)
        {
            if (amount <= 0)
            {
                _displayer.Display("Heal amount must be positive.");
                return;
            }
            else if (Health + amount > 100)
            {
                int curHealth = Health;
                Health = 100;
                _displayer.Display($"{Name} heals for {Health - curHealth}. Current health: {Health}");
            }
            else
            {
                Health += amount;
                _displayer.Display($"{Name} heals for {amount}. Current health: {Health}");
            }
        }

        public void EquipItem(IItem item)
        {
            _displayer.Display($"{Name} equips {item.Name}.");
            _equippedItems.Add(item);
            item.Equip(this, _displayer);
        }

        public void UseAbility(string abilityName, Character target)
        {
            var ability = _abilities.Find(a => a.Name == abilityName);

            if (ability != null)
            {
                _displayer.Display($"{Name} uses {ability.Name} on {target.Name}!");
                ability.Use(this, target, _displayer);
            }
            else
            {
                _displayer.Display($"{Name} doesn't know the ability '{abilityName}'.");
            }
        }

        public void ApplyStatModifier(int attackMod, int armorMod)
        {
            AttackPower += attackMod;
            Armor += armorMod;
        }

        public void RemoveStatModifier(int attackMod, int armorMod)
        {
            AttackPower -= attackMod;
            Armor -= armorMod;
        }

        public void AddAbility(IAbility ability)
        {
            if (!_abilities.Contains(ability))
            {
                _abilities.Add(ability);
                _displayer.Display($"{Name} learned a new ability: {ability.Name}!");
            }
            else
            {
                _displayer.Display($"{Name} already knows {ability.Name}.");
            }
        }

        /// <summary>
        /// Creates a Memento of the current state.
        /// </summary>
        public CharacterMemento SaveState()
        {
            var abilityNames = _abilities.Select(a => a.Name).ToList();
            var itemNames = _equippedItems.Select(i => i.Name).ToList();

            return new CharacterMemento(Health, Armor, AttackPower, abilityNames, itemNames);
        }

        /// <summary>
        /// Restores state with Memento.
        /// </summary>
        public void RestoreState(CharacterMemento memento)
        {
            Health = memento.Health;
            Armor = memento.Armor;
            AttackPower = memento.AttackPower;
        }

        public void RestoreState(int health, int armor, int attackPower)
        {
            Health = health;
            Armor = armor;
            AttackPower = attackPower;
        }
    }
}