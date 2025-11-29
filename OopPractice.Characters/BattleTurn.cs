namespace OopPractice.Characters
{
    public class BattleTurn
    {
        public int TurnNumber { get; set; }
        public string ActorName { get; set; } = "";
        public string TargetName { get; set; } = "";
        public string ActionType { get; set; } = "";
        public int Value { get; set; }
        public string Description { get; set; } = "";

        public override string ToString()
        {
            return $"Turn {TurnNumber}: {ActorName} used {ActionType} on {TargetName} -> {Value} ({Description})";
        }
    }
}