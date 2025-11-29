using OopPractice.Characters;
using OopPractice.Display;

namespace OopPractice1.Strategies
{
    public class GameStrategy : ICommandStrategy
    {
        private readonly IDisplayer _displayer;
        private GameSession? _currentSession;

        public GameStrategy(IDisplayer displayer)
        {
            _displayer = displayer;
        }

        public void RegisterCommands(CliManager manager)
        {
            manager.RegisterCommand("start-game", StartGame);
            manager.RegisterCommand("battle-log", ShowBattleLog);
        }

        private void StartGame(string[] args)
        {
            var player = new Warrior("PlayerHero", _displayer);
            var enemy = new Mage("DarkWizard", _displayer);

            _displayer.Display($"Starting battle: {player.Name} vs {enemy.Name}");

            _currentSession = new GameSession(
                new List<Character> { player },
                new List<Character> { enemy },
                _displayer
            );

            _currentSession.StartBattle();
        }

        private void ShowBattleLog(string[] args)
        {
            if (_currentSession == null)
            {
                _displayer.Display("No game played yet.");
                return;
            }

            _displayer.Display("\n--- Battle History ---");
            foreach (var turn in _currentSession.History)
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(turn.ToString());
                Console.ResetColor();
            }
        }
    }
}