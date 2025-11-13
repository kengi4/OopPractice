using OopPractice.Text;
using OopPractice.Characters;

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
Console.WriteLine("\n\n--- Game System ---");
Console.WriteLine();

Character warrior = new Warrior("Aragorn");
Character mage = new Mage("Gandalf");
Console.WriteLine($"Created: {warrior.Name} (HP: {warrior.Health}, AP: {warrior.AttackPower}, Armor: {warrior.Armor})");
Console.WriteLine($"Created: {mage.Name} (HP: {mage.Health}, AP: {mage.AttackPower}, Armor: {mage.Armor})");
Console.WriteLine();

IItem sword = new Sword();

warrior.EquipItem(sword);
Console.WriteLine($"Current stats for {warrior.Name}: (AP: {warrior.AttackPower})");
Console.WriteLine();

mage.UseAbility("Fireball", warrior);
Console.WriteLine();

warrior.UseAbility("Power Strike", mage);
Console.WriteLine();

warrior.Attack(mage);
Console.WriteLine();

mage.Attack(warrior);
Console.WriteLine();
Console.WriteLine("\nPress any key to exit...");
Console.ReadKey();