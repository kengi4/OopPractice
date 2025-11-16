using OopPractice.Characters;
using OopPractice.Text;
using OopPractice1;

ILogger logger = new ConsoleLogger();
CliManager cliManager = new CliManager();

string mode = ParseArguments(args);

if (mode == "text")
{
    logger.Log("Starting in Text Mode...");
    var textHandler = new TextCommandHandler(logger);
    textHandler.RegisterCommands(cliManager);
}
else if (mode == "chars")
{
    logger.Log("Starting in Characters Mode...");
    var charHandler = new CharacterCommandHandler(logger);
    charHandler.RegisterCommands(cliManager);
}
else
{
    logger.Log("No valid mode selected. Exiting.");
    return;
}

cliManager.Run();

logger.Log("Exiting application.");


static string ParseArguments(string[] args)
{
    if (args.Contains("--text"))
    {
        return "text";
    }
    if (args.Contains("--chars"))
    {
        return "chars";
    }

    Console.WriteLine("No mode selected. Defaulting to 'Text' mode.");
    Console.WriteLine("Use '--text' or '--chars' to select a mode.");
    return "text";
}