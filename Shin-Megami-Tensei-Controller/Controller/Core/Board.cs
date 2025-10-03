namespace Shin_Megami_Tensei
{
    public class Board
    {
        private readonly Dictionary<string, UnitBase?> _playerOneBoard;
        private readonly Dictionary<string, UnitBase?> _playerTwoBoard;

        // Roster original (orden exacto del archivo)
        private readonly List<UnitBase> _playerOneRoster;
        private readonly List<UnitBase> _playerTwoRoster;

        public Board(List<UnitBase> playerOneUnits, List<UnitBase> playerTwoUnits)
        {
            _playerOneRoster = new List<UnitBase>(playerOneUnits);
            _playerTwoRoster = new List<UnitBase>(playerTwoUnits);

            _playerOneBoard = InitializeBoard(playerOneUnits);
            _playerTwoBoard = InitializeBoard(playerTwoUnits);
        }

        private static Dictionary<string, UnitBase?> InitializeBoard(List<UnitBase> teamUnits)
        {
            var board = new Dictionary<string, UnitBase?>(GameConstants.BoardPositions.Length);
            for (var i = 0; i < GameConstants.BoardPositions.Length; i++)
            {
                var position = GameConstants.BoardPositions[i];
                board[position] = i < teamUnits.Count ? teamUnits[i] : null;
            }
            return board;
        }

        // Board mutable por jugador
        public Dictionary<string, UnitBase?> SelectMutableBoard(int playerId)
            => playerId == 1 ? _playerOneBoard : _playerTwoBoard;

        public IReadOnlyDictionary<string, UnitBase?> GetBoardForPlayer(int playerId)
            => SelectMutableBoard(playerId);

        public UnitBase GetTeamLeaderUnit(int playerId)
            => GetBoardForPlayer(playerId)[GameConstants.BoardPositions[0]]!;

        // 🔹 Calcula la reserva dinámicamente: roster – board
        public List<UnitBase> GetReserveUnitsForPlayer(int playerId)
        {
            var roster = GetRoster(playerId);
            var boardUnits = GetBoardForPlayer(playerId).Values.Where(u => u != null).ToHashSet();
            return roster.Where(unit => !boardUnits.Contains(unit)).ToList();
        }

        // Unidades vivas en tablero
        public List<UnitBase> GetAliveUnits(int playerId)
        {
            return GetBoardForPlayer(playerId)
                .Values
                .Where(unit => unit is { Stats.HP: > 0 })
                .Cast<UnitBase>()
                .ToList();
        }

        // Roster original
        public IReadOnlyList<UnitBase> GetRoster(int playerId)
            => playerId == 1 ? _playerOneRoster : _playerTwoRoster;

        // Manejo de muertes (quitar del board, vuelve implícitamente a la reserva)
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
            // No hay que tocar reservas: al no estar en el board,
            // automáticamente aparece en GetReserveUnitsForPlayer()
        }

        // Helpers
        private static bool IsMonsterAtPosition(Dictionary<string, UnitBase?> board, string pos, UnitBase monster)
            => ReferenceEquals(board[pos], monster);

        // Estado de la partida
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
