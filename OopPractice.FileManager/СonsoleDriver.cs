using System.Text;

namespace OopPractice.FileManager
{
    public interface IConsoleDriver
    {
        void Clear();
        void WriteAt(int x, int y, string text);
        void SetColor(ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black);
        void ResetColor();
        ConsoleKeyInfo ReadKey();
        int WindowHeight { get; }
        int WindowWidth { get; }
        void HideCursor();
        void ShowCursor();
    }

    public class SystemConsoleDriver : IConsoleDriver
    {
        public int WindowHeight => Console.WindowHeight;
        public int WindowWidth => Console.WindowWidth;

        public void Clear()
        {
            Console.ResetColor();
            Console.Clear();
        }

        public void WriteAt(int x, int y, string text)
        {
            try
            {
                if (x >= 0 && x < WindowWidth && y >= 0 && y < WindowHeight)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(text);
                }
            }
            catch { }
        }

        public void SetColor(ConsoleColor foreground, ConsoleColor background = ConsoleColor.Black)
        {
            Console.ForegroundColor = foreground;
            Console.BackgroundColor = background;
        }

        public void ResetColor() => Console.ResetColor();

        public ConsoleKeyInfo ReadKey() => Console.ReadKey(true);
        public void HideCursor() => Console.CursorVisible = false;
        public void ShowCursor() => Console.CursorVisible = true;
    }
}