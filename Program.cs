using OopPractice.Characters;
using OopPractice.Text;
using OopPractice1;

// Text
Console.WriteLine("--- Text System ---");
Console.WriteLine();

TextFactory doc = new TextFactory("Test Document");

doc.AddParagraph("Some thoughts...");
doc.AddHeading("Top:");
doc.AddParagraph("This is a line.");
doc.AddParagraph("This is another line.");
doc.AddHeading("Inner:");
doc.AddParagraph("This is an inner line.");
doc.Up();
doc.AddHeading("Inner 2:");
doc.AddParagraph("Another inner line.");
doc.Up();
doc.Up();
doc.AddParagraph("Some closure...");

Console.WriteLine(doc.ToString());

// Game
ILogger logger = new ConsoleLogger();

Character hero = new Warrior("Aragorn", logger);
Character mage = new Mage("Gandalf", logger);

Game battle = new Game(hero, mage, logger);

battle.SimulateBattle();

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();