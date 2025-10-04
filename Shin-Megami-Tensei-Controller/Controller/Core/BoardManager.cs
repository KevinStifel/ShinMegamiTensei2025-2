namespace Shin_Megami_Tensei
{
    public class BoardManager
    {
        private readonly Board _board;

        public BoardManager(Board board)
        {
            _board = board;
        }

        public Dictionary<string, UnitBase?> SelectMutableBoard(int playerId)
            => playerId == 1 ? _board.PlayerOneBoard : _board.PlayerTwoBoard;

        public IReadOnlyDictionary<string, UnitBase?> GetBoardForPlayer(int playerId)
            => SelectMutableBoard(playerId);

        public UnitBase GetTeamLeaderUnit(int playerId)
            => GetBoardForPlayer(playerId)[GameConstants.BoardPositions[0]]!;

        public List<UnitBase> GetReserveUnitsForPlayer(int playerId)
        {
            var roster = GetRoster(playerId);
            var boardUnits = GetBoardForPlayer(playerId).Values.Where(u => u != null).ToHashSet();
            return roster.Where(unit => !boardUnits.Contains(unit)).ToList();
        }

        public List<UnitBase> GetAliveUnits(int playerId)
        {
            return GetBoardForPlayer(playerId)
                .Values
                .Where(unit => unit is { Stats.HP: > 0 })
                .Cast<UnitBase>()
                .ToList();
        }

        private IReadOnlyList<UnitBase> GetRoster(int playerId)
            => playerId == 1 ? _board.PlayerOneRoster : _board.PlayerTwoRoster;

        public void HandleUnitDeath(int currentPlayerId, UnitBase unit)
        {
            if (unit is Samurai) return;
            RemoveMonsterFromBoard(currentPlayerId, unit);
        }

        private void RemoveMonsterFromBoard(int playerId, UnitBase monster)
        {
            var board = SelectMutableBoard(playerId);

            foreach (var pos in GameConstants.BoardPositions)
            {
                if (ReferenceEquals(board[pos], monster))
                {
                    board[pos] = null;
                    break;
                }
            }
        }
        public bool HasWinner()
        {
            return GetWinner() != BattleOutcome.Ongoing;
        }

        public BattleOutcome GetWinner()
        {
            if (IsDraw()) return BattleOutcome.Draw;
            if (HasPlayerTwoLost()) return BattleOutcome.PlayerOneWins;
            return HasPlayerOneLost() ? BattleOutcome.PlayerTwoWins : BattleOutcome.Ongoing;
        }

        private bool IsDraw()
        {
            return !IsPlayerAlive(1) && !IsPlayerAlive(2);
        }

        private bool HasPlayerOneLost()
        {
            return !IsPlayerAlive(1) && IsPlayerAlive(2);
        }

        private bool HasPlayerTwoLost()
        {
            return IsPlayerAlive(1) && !IsPlayerAlive(2);
        }

        private bool IsPlayerAlive(int playerId)
        {
            return GetAliveUnits(playerId).Count > 0;
        }
    }
}
