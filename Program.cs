using OopPractice.Characters;
using OopPractice.Text;
using OopPractice1;

// Text
Console.WriteLine("--- Text System ---");
Console.WriteLine();

Text myArticle = new Text();

myArticle.Add(new Header("The Principles of OOP", 1));
myArticle.Add(new Paragraph("Object-Oriented Programming (OOP) is a programming paradigm..."));
myArticle.Add(new Header("Inheritance", 2));
myArticle.Add(new Paragraph("Inheritance allows a class to inherit properties..."));
myArticle.Add(new Header("Polymorphism", 2));
myArticle.Add(new Paragraph("Polymorphism allows..."));

Console.WriteLine("=== Rendering Table of Contents ===");
Console.WriteLine(myArticle.RenderTableOfContents());

Console.WriteLine("=== Rendering Full Text ===");
Console.WriteLine(myArticle.RenderFullText());

// Game
ILogger logger = new ConsoleLogger();

Character hero = new Warrior("Aragorn", logger);
Character mage = new Mage("Gandalf", logger);

Game battle = new Game(hero, mage, logger);

battle.SimulateBattle();

Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();