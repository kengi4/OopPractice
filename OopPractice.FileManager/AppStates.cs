namespace OopPractice.FileManager
{
    public class FileManagerContext
    {
        private IAppState _currentState;
        public IConsoleDriver Driver { get; }
        public bool IsRunning { get; set; } = true;

        public string CurrentPath { get; set; }

        public FileManagerContext(IConsoleDriver driver)
        {
            Driver = driver;
            CurrentPath = Directory.GetCurrentDirectory();
            _currentState = new DirectoryState(this, CurrentPath);
        }

        public void ChangeState(IAppState newState)
        {
            _currentState = newState;
            Driver.Clear();
        }

        public void Run()
        {
            Driver.HideCursor();
            while (IsRunning)
            {
                _currentState.Render();
                var input = Driver.ReadKey();
                _currentState.HandleInput(input);
            }
            Driver.ShowCursor();
            Driver.Clear();
        }
    }

    // Інтерфейс Стану
    public interface IAppState
    {
        void Render();
        void HandleInput(ConsoleKeyInfo input);
    }
}