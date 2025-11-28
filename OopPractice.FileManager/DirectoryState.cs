namespace OopPractice.FileManager
{
    public class DirectoryState : IAppState
    {
        private readonly FileManagerContext _context;
        private List<string> _entries = new List<string>();
        private int _selectedIndex = 0;
        private string _path;

        public DirectoryState(FileManagerContext context, string path)
        {
            _context = context;
            _path = path;
            LoadEntries();
        }

        private void LoadEntries()
        {
            try
            {
                _entries = new List<string>();
                _entries.Add("..");
                _entries.AddRange(Directory.GetDirectories(_path));
                _entries.AddRange(Directory.GetFiles(_path));
            }
            catch (Exception ex)
            {
                _entries = new List<string> { "Error: " + ex.Message, ".." };
            }
        }

        public void Render()
        {
            _context.Driver.SetColor(ConsoleColor.Yellow);
            string header = $"DIR: {_path}".PadRight(_context.Driver.WindowWidth);
            _context.Driver.WriteAt(0, 0, header);

            _context.Driver.WriteAt(0, 1, new string('-', _context.Driver.WindowWidth));
            _context.Driver.ResetColor();

            int startY = 2;
            int maxLines = _context.Driver.WindowHeight - 3;

            int startIdx = Math.Max(0, _selectedIndex - maxLines / 2);
            int itemsToEnd = Math.Min(_entries.Count - startIdx, maxLines);

            for (int i = 0; i < itemsToEnd; i++)
            {
                int entryIndex = startIdx + i;
                string entry = _entries[entryIndex];

                string displayName = Path.GetFileName(entry);
                if (string.IsNullOrEmpty(displayName)) displayName = entry;

                string lineText = displayName.PadRight(_context.Driver.WindowWidth);

                if (entryIndex == _selectedIndex)
                {
                    _context.Driver.SetColor(ConsoleColor.Black, ConsoleColor.Gray);
                    _context.Driver.WriteAt(0, startY + i, $"> {lineText}");
                    _context.Driver.ResetColor();
                }
                else
                {
                    bool isDir = Directory.Exists(entry);
                    _context.Driver.SetColor(isDir ? ConsoleColor.DarkYellow : ConsoleColor.Gray);
                    _context.Driver.WriteAt(0, startY + i, $"  {lineText}");
                }
            }
            _context.Driver.ResetColor();
            for (int i = itemsToEnd; i < maxLines; i++)
            {
                _context.Driver.WriteAt(0, startY + i, new string(' ', _context.Driver.WindowWidth));
            }

            _context.Driver.SetColor(ConsoleColor.DarkGreen);
            string footer = " Enter: Open | Esc: Exit ".PadRight(_context.Driver.WindowWidth);
            _context.Driver.WriteAt(0, _context.Driver.WindowHeight - 1, footer);
            _context.Driver.ResetColor();
        }

        public void HandleInput(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.UpArrow:
                    if (_selectedIndex > 0) _selectedIndex--;
                    break;
                case ConsoleKey.DownArrow:
                    if (_selectedIndex < _entries.Count - 1) _selectedIndex++;
                    break;
                case ConsoleKey.Enter:
                    OpenSelected();
                    break;
                case ConsoleKey.Escape:
                    _context.IsRunning = false;
                    break;
            }
        }

        private void OpenSelected()
        {
            string selected = _entries[_selectedIndex];
            if (selected == "..")
            {
                var parent = Directory.GetParent(_path);
                if (parent != null)
                {
                    _context.ChangeState(new DirectoryState(_context, parent.FullName));
                }
            }
            else if (Directory.Exists(selected))
            {
                _context.ChangeState(new DirectoryState(_context, selected));
            }
            else if (File.Exists(selected))
            {
                _context.ChangeState(new FileViewState(_context, selected));
            }
        }
    }
}