using OopPractice1;
using OopPractice.Display;
using OopPractice.FileManager;

IDisplayer displayer = new ConsoleDisplayer();
CliManager cliManager = new CliManager();

string mode = ParseArguments(args);

if (mode == "text")
{
    displayer.Display("Text mode update pending...");
}
else if (mode == "chars")
{
    displayer.Display("Starting in Characters Mode...");
    var charHandler = new CharacterCommandHandler(displayer);
    charHandler.RegisterCommands(cliManager);
}
else if (mode == "files")
{
    IConsoleDriver driver = new SystemConsoleDriver();
    FileManagerContext context = new FileManagerContext(driver);
    context.Run();

    Console.Clear();
    return;
}
else
{
    displayer.Display("No valid mode selected. Exiting.");
    return;
}

cliManager.Run();

displayer.Display("Exiting application.");

static string ParseArguments(string[] args)
{
    if (args.Contains("--text")) return "text";
    if (args.Contains("--chars")) return "chars";
    if (args.Contains("--files")) return "files";

    Console.WriteLine("No mode selected. Available modes:");
    Console.WriteLine(" --chars : RPG Character Manager");
    Console.WriteLine(" --text  : Text Document Composite");
    Console.WriteLine(" --files : File Manager");
    return "text";
}