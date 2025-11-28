using OopPractice.Characters;
using OopPractice.Text;
using OopPractice.Display;

namespace OopPractice1
{
    /// <summary>
    /// Handles all logic for the 'Text' CLI mode.
    /// </summary>
    public class TextCommandHandler
    {
        private readonly IDisplayer _displayer;
        private readonly TextFactory _factory;

        public TextCommandHandler(IDisplayer displayer)
        {
            _displayer = displayer;
            _factory = new TextFactory("My Document");
        }

        public void RegisterCommands(CliManager manager)
        {
            manager.RegisterCommand("pwd", PrintWorkingDirectory);
            manager.RegisterCommand("print", Print);
            manager.RegisterCommand("add", Add);
            manager.RegisterCommand("rm", Remove);
            manager.RegisterCommand("up", Up);
            manager.RegisterCommand("cd", ChangeDirectory);
        }

        private void PrintWorkingDirectory(string[] args)
        {
            var path = new Stack<string>();
            Container? node = _factory.CurrentNode;

            while (node != null && node.Parent != null)
            {
                path.Push(node.Name);
                node = node.Parent;
            }

            string pathString = "/" + string.Join("/", path);
            _displayer.Display(pathString);
        }
        private (List<string> positionalArgs, Dictionary<string, string> options) ParseArgs(string[] args)
        {
            var positionalArgs = new List<string>();
            var options = new Dictionary<string, string>();
            string? currentOption = null;

            foreach (var arg in args)
            {
                if (arg.StartsWith("--") || arg.StartsWith("-"))
                {
                    if (currentOption != null) options[currentOption] = "true";
                    currentOption = arg;
                }
                else if (currentOption != null)
                {
                    options[currentOption] = arg;
                    currentOption = null;
                }
                else
                {
                    positionalArgs.Add(arg);
                }
            }
            if (currentOption != null) options[currentOption] = "true";
            return (positionalArgs, options);
        }

        private void Print(string[] args)
        {
            var (positional, options) = ParseArgs(args);
            bool showIds = options.ContainsKey("--id");

            var visitor = new PlainTextVisitor(showIds);

            if (options.ContainsKey("--whole"))
            {
                _displayer.Display("--- Printing Whole Document (via Visitor) ---");
                _factory.RootNode.Accept(visitor);
            }
            else
            {
                _displayer.Display("--- Printing Current Node (via Visitor) ---");
                _factory.CurrentNode.Accept(visitor);
            }
            _displayer.Display(visitor.GetResult());
        }

        private string Prompt(string message)
        {
            Console.Write(message + " ");
            return Console.ReadLine() ?? string.Empty;
        }

        private void Add(string[] args)
        {
            if (args.Length < 2)
            {
                _displayer.Display("Usage: add <container|leaf> <name_or_content>");
                return;
            }

            string type = args[0].ToLower();
            string name = args[1];

            if (type == "container")
            {
                _factory.AddHeading(name);
                _displayer.Display($"Added heading: {name}");
            }
            else if (type == "leaf")
            {
                _factory.AddParagraph(name);
                _displayer.Display($"Added paragraph: {name}");
            }
        }

        private void Up(string[] args)
        {
            _factory.Up();
            _displayer.Display($"Moved up.");
        }

        private void Remove(string[] args)
        {
            Container currentNode = _factory.CurrentNode;

            if (args.Length == 0)
            {
                if (currentNode.Parent == null)
                {
                    _displayer.Display("Error: Cannot remove the root element.");
                    return;
                }

                string nodeName = GetNodeName(currentNode);
                string answer = Prompt($"Are you sure you want to remove '{nodeName}' and move up? (y/n):").ToLower();
                if (answer != "y")
                {
                    _displayer.Display("Removal cancelled.");
                    return;
                }

                _displayer.Display($"Removing current node '{nodeName}'...");
                currentNode.Parent.RemoveChild(currentNode);
                Up(Array.Empty<string>());
            }
            else
            {
                string nameToRemove = args[0];
                IText? childToRemove = currentNode.FindChild(nameToRemove);

                if (childToRemove != null)
                {
                    if (currentNode.RemoveChild(childToRemove))
                    {
                        _displayer.Display($"Removed element '{nameToRemove}' from '{GetNodeName(currentNode)}'.");
                    }
                    else
                    {
                        _displayer.Display($"Error: Failed to remove element '{nameToRemove}'.");
                    }
                }
                else
                {
                    _displayer.Display($"Error: Element '{nameToRemove}' not found in '{GetNodeName(currentNode)}'.");
                }
            }
        }

        private string GetNodeName(Container node)
        {
            if (node is Root) return "/ (root)";
            return node.Name;
        }

        private void ChangeDirectory(string[] args)
        {
            if (args.Length == 0)
            {
                _displayer.Display("Usage: cd <path> | .. | /");
                return;
            }
            string path = args[0];

            if (path == "/")
            {
                _factory.SetCurrentNode(_factory.RootNode);
                _displayer.Display("Moved to root.");
                PrintWorkingDirectory(Array.Empty<string>());
                return;
            }
            if (path == "..")
            {
                _factory.Up();
                _displayer.Display("Moved up.");
                PrintWorkingDirectory(Array.Empty<string>());
                return;
            }

            Container startNode = path.StartsWith("/") ? _factory.RootNode : _factory.CurrentNode;
            Container? targetNode = startNode;
            string[] parts = path.Trim('/').Split('/');

            foreach (string part in parts)
            {
                if (part == "..")
                {
                    targetNode = targetNode?.Parent;
                }
                else
                {
                    IText? foundChild = targetNode?.FindChild(part);
                    if (foundChild is Container container)
                    {
                        targetNode = container;
                    }
                    else
                    {
                        targetNode = null;
                        break;
                    }
                }
                if (targetNode == null) break;
            }

            if (targetNode != null)
            {
                _factory.SetCurrentNode(targetNode);
                _displayer.Display("Path changed.");
                PrintWorkingDirectory(Array.Empty<string>());
            }
            else
            {
                _displayer.Display($"Error: Path not found: '{path}'");
            }
        }
    }
}