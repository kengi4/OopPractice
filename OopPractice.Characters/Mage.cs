using OopPractice.Characters;
using OopPractice.Display;

/// <summary>
/// Represents a Mage, a specialized type of <see cref="Character"/>.
/// </summary>
public class Mage : Character
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Mage"/> class.
    /// </summary>
    public Mage(string name, IDisplayer displayer)
        : base(name, 80, 3, 5, displayer)
    {                                 
        _abilities.Add(new Fireball());
    }

    // There is no need to override "Attack." The mage will strike with a staff.
}