using OopPractice.Display;

namespace OopPractice.Characters
{
    public class GameSession
    {
        private readonly List<Character> _teamA;
        private readonly List<Character> _teamB;
        private readonly IDisplayer _displayer;

        public List<BattleTurn> History { get; private set; } = new();
        public int CurrentTurnNumber { get; private set; } = 1;

        public GameSession(List<Character> teamA, List<Character> teamB, IDisplayer displayer)
        {
            _teamA = teamA;
            _teamB = teamB;
            _displayer = displayer;
        }

        public void StartBattle()
        {
            _displayer.Display("--- Battle Started! ---");
            while (_teamA.Any(c => c.Health > 0) && _teamB.Any(c => c.Health > 0))
            {
                ExecuteRound();
                CurrentTurnNumber++;

                if (CurrentTurnNumber > 20) break;
            }
            _displayer.Display("--- Battle Ended ---");
        }

        public void ExecuteRound()
        {
            var allCharacters = _teamA.Concat(_teamB).Where(c => c.Health > 0).ToList();

            foreach (var actor in allCharacters)
            {
                if (actor.Health <= 0) continue;

                var enemies = _teamA.Contains(actor) ? _teamB : _teamA;
                var target = enemies.FirstOrDefault(e => e.Health > 0);

                if (target != null)
                {
                    var rand = new Random();
                    int damageDone = 0;
                    string actionName = "Attack";

                    if (rand.NextDouble() > 0.7 && actor.Abilities.Any())
                    {
                        var ability = actor.Abilities.First();
                        actionName = ability.Name;
                        actor.UseAbility(ability.Name, target);
                        damageDone = 0;
                    }
                    else
                    {
                        int rawDamage = actor.AttackPower;
                        int mitigation = target.Armor;
                        damageDone = Math.Max(0, rawDamage - mitigation);

                        target.TakeDamage(rawDamage);
                    }

                    // Записуємо хід в історію
                    History.Add(new BattleTurn
                    {
                        TurnNumber = CurrentTurnNumber,
                        ActorName = actor.Name,
                        TargetName = target.Name,
                        ActionType = actionName,
                        Value = damageDone,
                        Description = $"{target.Name} has {target.Health} HP left."
                    });
                }
            }
        }
    }
}