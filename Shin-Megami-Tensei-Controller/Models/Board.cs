namespace Shin_Megami_Tensei
{
    public class Board
    {
        public Dictionary<string, UnitBase?> PlayerOneBoard { get; }
        public Dictionary<string, UnitBase?> PlayerTwoBoard { get; }

        public List<UnitBase> PlayerOneRoster { get; }
        public List<UnitBase> PlayerTwoRoster { get; }
        
        public Board(List<UnitBase> playerOneUnits, List<UnitBase> playerTwoUnits)
        {
            PlayerOneRoster = new List<UnitBase>(playerOneUnits);
            PlayerTwoRoster = new List<UnitBase>(playerTwoUnits);

            PlayerOneBoard = InitializeBoard(playerOneUnits);
            PlayerTwoBoard = InitializeBoard(playerTwoUnits);
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
    }
}