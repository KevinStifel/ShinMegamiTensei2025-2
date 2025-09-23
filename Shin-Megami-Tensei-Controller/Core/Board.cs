namespace Shin_Megami_Tensei
{
    public class Board
    {
        private readonly Dictionary<string, UnitBase?> _playerOneBoard;
        private readonly Dictionary<string, UnitBase?> _playerTwoBoard;
        private readonly List<UnitBase> _playerOneReserve;
        private readonly List<UnitBase> _playerTwoReserve;

        // E2: se usa para menús/órdenes en el mismo orden del archivo original (revivir, invocar)
        private readonly List<UnitBase> _playerOneRoster;
        private readonly List<UnitBase> _playerTwoRoster;

        public Board(List<UnitBase> playerOneUnits, List<UnitBase> playerTwoUnits)
        {
            _playerOneRoster = new List<UnitBase>(playerOneUnits); // E2
            _playerTwoRoster = new List<UnitBase>(playerTwoUnits); // E2

            _playerOneBoard = InitializeBoard(playerOneUnits, out _playerOneReserve);
            _playerTwoBoard = InitializeBoard(playerTwoUnits, out _playerTwoReserve);
        }

        private Dictionary<string, UnitBase?> InitializeBoard(List<UnitBase> teamUnits, out List<UnitBase> reserveUnits)
        {
            var board = new Dictionary<string, UnitBase?>(GameConstants.BoardPositions.Length);
            for (int i = 0; i < GameConstants.BoardPositions.Length; i++)
            {
                string pos = GameConstants.BoardPositions[i];
                board[pos] = i < teamUnits.Count ? teamUnits[i] : null;
            }

            reserveUnits = teamUnits.Count > GameConstants.BoardPositions.Length
                ? teamUnits.Skip(GameConstants.BoardPositions.Length).ToList()
                : new List<UnitBase>();

            return board;
        }

        private Dictionary<string, UnitBase?> SelectMutableBoard(int playerId)
            => playerId == 1 ? _playerOneBoard : _playerTwoBoard;

        public IReadOnlyDictionary<string, UnitBase?> GetBoardForPlayer(int playerId)
            => SelectMutableBoard(playerId);
        

        public UnitBase GetTeamLeaderUnit(int playerId)
            => GetBoardForPlayer(playerId)[GameConstants.BoardPositions[0]]!;

        private List<UnitBase> GetReserveForPlayer(int playerId)
            => playerId == 1 ? _playerOneReserve : _playerTwoReserve;

        public List<UnitBase> GetAliveUnits(int playerId)
        {
            return GetBoardForPlayer(playerId)
                .Values
                .Where(u => u != null && u.Stats.HP > 0)
                .Cast<UnitBase>()
                .ToList();
        }

        // E2: útil para construir menús en orden de archivo (revivir, invitation, sabbatma)
        public IReadOnlyList<UnitBase> GetRoster(int playerId)
            => playerId == 1 ? _playerOneRoster : _playerTwoRoster;
        

        public void HandleUnitDeath(int currentPlayerId, UnitBase unit)
        {
            if (unit is Samurai) return;
            RemoveMonsterFromBoardToReserve(currentPlayerId, unit);
        }

        private void RemoveMonsterFromBoardToReserve(int playerId, UnitBase monster)
        {
            var board = SelectMutableBoard(playerId);

            foreach (var pos in GameConstants.BoardPositions)
            {
                if (IsMonsterAtPosition(board, pos, monster))
                {
                    board[pos] = null;
                    break;
                }
            }

            var reserve = GetReserveForPlayer(playerId);
            if (IsAbsentFromReserve(reserve, monster))
            {
                reserve.Add(monster);
            }
        }

        private static bool IsMonsterAtPosition(Dictionary<string, UnitBase?> board, string pos, UnitBase monster)
            => ReferenceEquals(board[pos], monster);

        private static bool IsAbsentFromReserve(List<UnitBase> reserve, UnitBase monster)
            => !reserve.Contains(monster);
    }
}


    /*
    // (Útil si luego necesitas saber posición actual de una unidad)
    public bool TryGetPositionOfUnit(int playerId, UnitBase unit, out string position)
    {
        var board = GetBoardForPlayer(playerId);
        foreach (var kvp in board)
        {
            if (ReferenceEquals(kvp.Value, unit))
            {
                position = kvp.Key;
                return true;
            }
        }
        position = string.Empty;
        return false;
    }

    // (Opcional, útil E2) roster para menús que piden orden del archivo
    public IReadOnlyList<UnitBase> GetRoster(int playerId)
        => playerId == 1 ? _playerOneRoster : _playerTwoRoster;
    */
