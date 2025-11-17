using OopPractice.Display;

namespace OopPractice.Characters
{
    /// <summary>
    /// Manages the game flow and simulation.
    /// </summary>
    public class Game
    {
        private readonly Character _player1;
        private readonly Character _player2;
        private readonly IDisplayer _displayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="player1">The first character.</param>
        /// <param name="player2">The second character.</param>
        /// <param name="displayer">The display service.</param>
        public Game(Character player1, Character player2, IDisplayer displayer)
        {
            _player1 = player1;
            _player2 = player2;
            _displayer = displayer;
        }

        /// <summary>
        /// Runs the entire battle simulation.
        /// (Запускає повну симуляцію бою)
        /// </summary>
        public void SimulateBattle()
        {
            _displayer.Display("\n\n--- Game System ---"); 
            _displayer.Display(""); 

            _displayer.Display($"Created: {_player1.Name} (HP: {_player1.Health}, AP: {_player1.AttackPower}, Armor: {_player1.Armor})"); 
            _displayer.Display($"Created: {_player2.Name} (HP: {_player2.Health}, AP: {_player2.AttackPower}, Armor: {_player2.Armor})"); 
            _displayer.Display(""); 

            IItem sword = new Sword();

            _player1.EquipItem(sword); 
            _displayer.Display($"Current stats for {_player1.Name}: (AP: {_player1.AttackPower})"); 
            _displayer.Display(""); 

            _player2.UseAbility("Fireball", _player1); 
            _displayer.Display(""); 

            _player1.UseAbility("Power Strike", _player2); 
            _displayer.Display(""); 

            _player1.Attack(_player2); 
            _displayer.Display(""); 

            _player2.Attack(_player1); 
            _displayer.Display(""); 
        }
    }
}