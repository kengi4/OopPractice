using OopPractice1;
using OopPractice.Display;

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
else
{
    displayer.Display("No valid mode selected. Exiting.");
    return;
}

cliManager.Run();

displayer.Display("Exiting application.");


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