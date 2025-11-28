namespace OopPractice.FileManager
{
    public class FileEditState : IAppState
    {
        private readonly FileManagerContext _context;
        private readonly string _filePath;
        private List<string> _lines;
        private int _cursorX = 0;
        private int _cursorY = 0;
        private int _scrollOffset = 0;

        public FileEditState(FileManagerContext context, string filePath, List<string> lines)
        {
            _context = context;
            _filePath = filePath;
            _lines = lines;
            if (_lines.Count == 0) _lines.Add("");
        }

        public void Render()
        {
            _context.Driver.Clear();
            _context.Driver.SetColor(ConsoleColor.Red);
            _context.Driver.WriteAt(0, 0, $"EDIT: {Path.GetFileName(_filePath)} *");
            _context.Driver.ResetColor();
            _context.Driver.WriteAt(0, 1, new string('=', _context.Driver.WindowWidth));

            int maxDisplayLines = _context.Driver.WindowHeight - 3;

            if (_cursorY < _scrollOffset) _scrollOffset = _cursorY;
            if (_cursorY >= _scrollOffset + maxDisplayLines) _scrollOffset = _cursorY - maxDisplayLines + 1;

            for (int i = 0; i < maxDisplayLines; i++)
            {
                int lineIdx = _scrollOffset + i;
                if (lineIdx < _lines.Count)
                {
                    _context.Driver.WriteAt(0, 2 + i, _lines[lineIdx]);
                }
            }

            _context.Driver.SetColor(ConsoleColor.DarkGray);
            _context.Driver.WriteAt(0, _context.Driver.WindowHeight - 1, $"Ln: {_cursorY + 1} Col: {_cursorX + 1} | F10: Save | Esc: Cancel");
            _context.Driver.ResetColor();

            _context.Driver.ShowCursor();
            _context.Driver.WriteAt(_cursorX, 2 + (_cursorY - _scrollOffset), "");
        }

        public void HandleInput(ConsoleKeyInfo input)
        {
            if (input.Key == ConsoleKey.UpArrow)
            {
                if (_cursorY > 0) _cursorY--;
                _cursorX = Math.Min(_cursorX, _lines[_cursorY].Length);
            }
            else if (input.Key == ConsoleKey.DownArrow)
            {
                if (_cursorY < _lines.Count - 1) _cursorY++;
                _cursorX = Math.Min(_cursorX, _lines[_cursorY].Length);
            }
            else if (input.Key == ConsoleKey.LeftArrow)
            {
                if (_cursorX > 0) _cursorX--;
            }
            else if (input.Key == ConsoleKey.RightArrow)
            {
                if (_cursorX < _lines[_cursorY].Length) _cursorX++;
            }
            else if (input.Key == ConsoleKey.Backspace)
            {
                if (_cursorX > 0)
                {
                    string line = _lines[_cursorY];
                    _lines[_cursorY] = line.Remove(_cursorX - 1, 1);
                    _cursorX--;
                }
                else if (_cursorY > 0)
                {
                    string currentLine = _lines[_cursorY];
                    string prevLine = _lines[_cursorY - 1];
                    _lines[_cursorY - 1] = prevLine + currentLine;
                    _cursorX = prevLine.Length;
                    _lines.RemoveAt(_cursorY);
                    _cursorY--;
                }
            }
            else if (input.Key == ConsoleKey.Enter)
            {
                string currentLine = _lines[_cursorY];
                string nextPart = currentLine.Substring(_cursorX);
                _lines[_cursorY] = currentLine.Substring(0, _cursorX);
                _lines.Insert(_cursorY + 1, nextPart);
                _cursorY++;
                _cursorX = 0;
            }
            else if (input.Key == ConsoleKey.F10)
            {
                try
                {
                    File.WriteAllLines(_filePath, _lines);

                    _context.Driver.WriteAt(0, 0, $"SAVED: {Path.GetFileName(_filePath)}  ");
                    Thread.Sleep(500); 

                    _context.ChangeState(new FileViewState(_context, _filePath));
                }
                catch (Exception ex)
                {
                    _lines.Add($"ERROR SAVING: {ex.Message}");
                }
            }
            else if (input.Key == ConsoleKey.Escape)
            {
                 _context.ChangeState(new FileViewState(_context, _filePath));
            }
            else if (!char.IsControl(input.KeyChar))
            {
                string line = _lines[_cursorY];
                _lines[_cursorY] = line.Insert(_cursorX, input.KeyChar.ToString());
                _cursorX++;
            }
        }
    }
}