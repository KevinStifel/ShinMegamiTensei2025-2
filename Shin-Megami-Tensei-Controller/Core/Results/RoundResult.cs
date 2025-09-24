namespace Shin_Megami_Tensei
{
    public enum RoundOutcome
    {
        Ongoing = 0,
        BattleEnded = 1
    }

    public readonly struct RoundResult
    {
        private RoundResult(RoundOutcome outcome, int winnerId)
        {
            Outcome = outcome;
            WinnerId = winnerId; // 0 = empate, 1 o 2 = ganador, -1 = sin ganador
        }

        private RoundOutcome Outcome { get; }
        public int WinnerId { get; }

        public bool DidBattleEnd => Outcome == RoundOutcome.BattleEnded;

        public static RoundResult Ongoing() =>
            new(RoundOutcome.Ongoing, -1);

        public static RoundResult BattleEnded(int winnerId) =>
            new(RoundOutcome.BattleEnded, winnerId);
    }
}