using OopPractice.Characters;

/// <summary>
/// Represents a base character in the game.
/// </summary>
public class Character
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Armor { get; private set; }
    public int AttackPower { get; private set; }
    public IEnumerable<IItem> EquippedItems => _equippedItems;
    public IEnumerable<IAbility> Abilities => _abilities;

    private readonly ILogger _logger;

    protected List<IAbility> _abilities = new List<IAbility>();
    protected List<IItem> _equippedItems = new List<IItem>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Character"/> class.
    /// </summary>
    public Character(string name, int health, int armor, int attackPower, ILogger logger)
    {
        Name = name;
        Health = health;
        Armor = armor;
        AttackPower = attackPower;
        _logger = logger;
    }

    /// <summary>
    /// Attacks another character.
    /// </summary>
    /// <param name="target">The character to attack.</param>
    public virtual void Attack(Character target)
    {
        _logger.Log($"{Name} attacks {target.Name}!");

        target.TakeDamage(this.AttackPower);
    }

    /// <summary>
    /// Reduces health based on incoming damage, considering armor.
    /// </summary>
    /// <param name="amount">The amount of damage to take.</param>
    public virtual void TakeDamage(int amount)
    {
        int damageTaken = Math.Max(0, amount - Armor);
        Health -= damageTaken;
        _logger.Log($"{Name} takes {damageTaken} damage. Current health: {Health}");
        if (Health <= 0)
        {
            _logger.Log($"{Name} has been defeated!");
        }
    }

    /// <summary>
    /// Heals the character for a specific amount.
    /// /// </summary>
    /// <param name="amount">The amount to heal.</param>
    public void Heal(int amount)
    {
        if (amount <= 0)
        {
            _logger.Log("Heal amount must be positive.");
            return;
        }
        else if (Health + amount > 100)
        {
            int curHealth = Health;
            Health = 100;
            _logger.Log($"{Name} heals for {Health - curHealth}. Current health: {Health}");
        }
        else
        {
            Health += amount;
            _logger.Log($"{Name} heals for {amount}. Current health: {Health}");
        }
    }

    /// <summary>
    /// Equips an item and applies its effects.
    /// </summary>
    /// <param name="item">The item to equip.</param>
    public void EquipItem(IItem item)
    {
        _logger.Log($"{Name} equips {item.Name}.");
        _equippedItems.Add(item);
        item.Equip(this, _logger);
    }

    /// <summary>
    /// Uses a specific ability.
    /// </summary>
    /// <param name="abilityName">The name of the ability to use.</param>
    /// <param name="target">The target of the ability.</param>
    public void UseAbility(string abilityName, Character target)
    {
        var ability = _abilities.Find(a => a.Name == abilityName);

        if (ability != null)
        {
            _logger.Log($"{Name} uses {ability.Name} on {target.Name}!");
            ability.Use(this, target, _logger);
        }
        else
        {
            _logger.Log($"{Name} doesn't know the ability '{abilityName}'.");
        }
    }

    /// <summary>
    /// Applies a temporary modifier to the character's stats.
    /// </summary>
    /// <param name="attackMod">The modifier for Attack Power.</param>
    /// <param name="armorMod">The modifier for Armor.</param>
    public void ApplyStatModifier(int attackMod, int armorMod)
    {
        AttackPower += attackMod;
        Armor += armorMod;
    }

    /// <summary>
    /// Removes a temporary modifier from the character's stats.
    /// </summary>
    /// <param name="attackMod">The modifier for Attack Power.</param>
    /// <param name="armorMod">The modifier for Armor.</param>
    public void RemoveStatModifier(int attackMod, int armorMod)
    {
        AttackPower -= attackMod;
        Armor -= armorMod;
    }

    /// <summary>
    /// Adds a new ability to the character's ability list.
    /// </summary>
    /// <param name="ability">The ability to add.</param>
    public void AddAbility(IAbility ability)
    {
        if (!_abilities.Contains(ability))
        {
            _abilities.Add(ability);
            _logger.Log($"{Name} learned a new ability: {ability.Name}!");
        }
        else
        {
            _logger.Log($"{Name} already knows {ability.Name}.");
        }
    }
}