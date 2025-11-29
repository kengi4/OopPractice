using System.Collections.Generic;
using System.Windows;
using OopPractice.Characters;
using OopPractice.Data;
using OopPractice.Display;
using System.Linq;

namespace OopPractice.WpfGui
{
    public partial class MainWindow : Window
    {
        private GameSession? _gameSession;
        private List<Character>? _characters;

        private readonly IDisplayer _displayer;
        private readonly Repository _repository;

        public MainWindow()
        {
            InitializeComponent();

            _displayer = new WpfDisplayer(LogBox);
            _repository = new Repository(_displayer);

            _displayer.Display("Welcome to OOP RPG GUI!");
            _displayer.Display("Press 'Start' to begin or 'Load' to restore save.");
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            var player = new Warrior("GUI_Hero", _displayer);
            var enemy = new Mage("Evil_AI", _displayer);

            StartGameLogic(new List<Character> { player, enemy });
        }

        private void BtnNextTurn_Click(object sender, RoutedEventArgs e)
        {
            if (_gameSession == null) return;


            _gameSession.ExecuteRound();

            var lastTurn = _gameSession.History.LastOrDefault();
            if (lastTurn != null)
            {
                StatusText.Text = $"Turn {_gameSession.CurrentTurnNumber}: {lastTurn.Description}";
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_characters != null)
            {
                _repository.SaveGame(_characters);
                MessageBox.Show("Game Saved Successfully!", "System");
            }
            else
            {
                MessageBox.Show("No active game to save.", "Error");
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            var data = _repository.LoadGame();
            if (data.Count > 0)
            {
                LogBox.Clear();
                _displayer.Display("[System] Loading save file...");

                var loadedCharacters = new List<Character>();
                foreach (var dto in data)
                {
                    Character c = dto.Type == "Mage"
                        ? new Mage(dto.Name, _displayer)
                        : new Warrior(dto.Name, _displayer);

                    c.RestoreState(dto.Health, dto.Armor, dto.AttackPower);


                    loadedCharacters.Add(c);
                    _displayer.Display($"- Restored: {c.Name} [{c.GetType().Name}] HP: {c.Health}");
                }

                StartGameLogic(loadedCharacters);

                StatusText.Text = "Game Loaded & Resumed.";
            }
            else
            {
                MessageBox.Show("Save file not found or empty.", "Error");
            }
        }

        private void StartGameLogic(List<Character> characters)
        {
            _characters = characters;

            var teamA = new List<Character> { _characters[0] };
            var teamB = _characters.Skip(1).ToList();

            _gameSession = new GameSession(teamA, teamB, _displayer);

            _displayer.Display($"=== SESSION STARTED ===");
            _displayer.Display($"Heroes: {teamA.Count} vs Enemies: {teamB.Count}");

            BtnNextTurn.IsEnabled = true;
            StatusText.Text = "Ready for battle.";
        }
    }
}