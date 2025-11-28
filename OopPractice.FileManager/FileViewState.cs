namespace OopPractice.FileManager
{
    public class FileViewState : IAppState
    {
        private readonly FileManagerContext _context;
        private readonly string _filePath;
        private string[] _lines;
        private int _scrollOffset = 0;

        public FileViewState(FileManagerContext context, string filePath)
        {
            _context = context;
            _filePath = filePath;
            try
            {
                _lines = File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                _lines = new[] { "Error reading file:", ex.Message };
            }
        }

        public void Render()
        {
            _context.Driver.Clear();
            _context.Driver.SetColor(ConsoleColor.Cyan);
            _context.Driver.WriteAt(0, 0, $"VIEW: {Path.GetFileName(_filePath)} (Lines: {_lines.Length})");
            _context.Driver.ResetColor();
            _context.Driver.WriteAt(0, 1, new string('-', _context.Driver.WindowWidth));

            int maxLines = _context.Driver.WindowHeight - 3;

            for (int i = 0; i < maxLines; i++)
            {
                int lineIdx = _scrollOffset + i;
                if (lineIdx < _lines.Length)
                {
                    _context.Driver.WriteAt(0, 2 + i, _lines[lineIdx]);
                }
            }

            _context.Driver.SetColor(ConsoleColor.DarkGray);
            _context.Driver.WriteAt(0, _context.Driver.WindowHeight - 1, "Arrows: Scroll | F2: Edit | Esc: Back");
            _context.Driver.ResetColor();
        }

        public void HandleInput(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (_scrollOffset > 0) _scrollOffset--;
                    break;
                case ConsoleKey.DownArrow:
                    if (_scrollOffset < _lines.Length - 1) _scrollOffset++;
                    break;
                case ConsoleKey.Escape:
                    string parentDir = Path.GetDirectoryName(_filePath) ?? Directory.GetDirectoryRoot(_filePath);
                    _context.ChangeState(new DirectoryState(_context, parentDir));
                    break;
                case ConsoleKey.F2:
                    _context.ChangeState(new FileEditState(_context, _filePath, _lines.ToList()));
                    break;
            }
        }
    }
}