using OopPractice.Characters;

/// <summary>
/// Represents a Mage, a specialized type of <see cref="Character"/>.
/// </summary>
public class Mage : Character
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mage"/> class.
    /// </summary>
    public Mage(string name)
        : base(name, 80, 3, 5)
    {
        _abilities.Add(new Fireball());
    }

    // There is no need to override "Attack." The mage will strike with a staff.
}