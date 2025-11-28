using OopPractice1;
using OopPractice1.Strategies;
using OopPractice.Display;
using OopPractice.FileManager;
using OopPractice.Renderers;

IConsoleDriver driver = new SystemConsoleDriver();
IDisplayer displayer = new ConsoleDisplayer();

var charRenderer = new CharacterConsoleRenderer(driver);

CliManager cli = new CliManager();

cli.UseStrategy(new CharacterManagementStrategy(charRenderer, displayer));
Console.WriteLine("=== OOP Practice CLI v2.0 ===");
Console.WriteLine("Loaded modules: Characters.");
cli.Run();