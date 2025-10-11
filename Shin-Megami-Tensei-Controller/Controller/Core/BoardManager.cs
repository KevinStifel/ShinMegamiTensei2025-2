namespace Shin_Megami_Tensei
{
    public class BoardManager
    {
        private readonly Board _board;
        
        private readonly Dictionary<int, PreparedSummonData> _preparedSummons = new();
        private record PreparedSummonData(UnitBase Monster, string Position, UnitBase? Replaced);

        public BoardManager(Board board)
        {
            _board = board;
        }
        
        public void PrepareSummonData(int playerId, UnitBase monster, string position, UnitBase? replaced)
        {
            _preparedSummons[playerId] = new PreparedSummonData(monster, position, replaced);
        }

        public (string Position, UnitBase? Replaced) GetPreparedSummonData(int playerId)
        {
            if (_preparedSummons.TryGetValue(playerId, out var data))
                return (data.Position, data.Replaced);

            throw new InvalidOperationException("No hay datos preparados para la invocación.");
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
        public List<UnitBase> GetAliveReserveUnitsForPlayer(int playerId)
        {
            return GetReserveUnitsForPlayer(playerId)
                .Where(unit => unit.Stats.HP > 0)
                .ToList();
        }
        
        public List<UnitBase> GetAliveUnits(int playerId)
        {
            return GetBoardForPlayer(playerId)
                .Values
                .Where(unit => unit is { Stats.HP: > 0 })
                .Cast<UnitBase>()
                .ToList();
        }
        public List<UnitBase> GetAllDeadUnits(int playerId)
        {
            var deadUnits = new List<UnitBase>();
            var leader = GetTeamLeaderUnit(playerId);
            
            if (leader.Stats.HP == 0)
                deadUnits.Add(leader);

            var reserveUnits = GetReserveUnitsForPlayer(playerId);
            foreach (var unit in reserveUnits)
            {
                if (unit.Stats.HP == 0)
                    deadUnits.Add(unit);
            }
            return deadUnits;
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

            foreach (var position in GameConstants.BoardPositions)
            {
                if (ReferenceEquals(board[position], monster))
                {
                    board[position] = null;
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
        public void RegisterPlayerSkillCounter(int playerId)
        {
            _board.SkillUseCounters.TryAdd(playerId, 0);
        }

        public int GetSkillUseCount(int playerId)
        {
            _board.SkillUseCounters.TryAdd(playerId, 0);
            return _board.SkillUseCounters[playerId];
        }

        public void IncrementSkillUseCount(int playerId)
        {
            _board.SkillUseCounters.TryAdd(playerId, 0);
            _board.SkillUseCounters[playerId]++;
        }
        public void ResetPlayerSkillCounter(int playerId)
        {
            _board.SkillUseCounters[playerId] = 0;
        }
    }
}
