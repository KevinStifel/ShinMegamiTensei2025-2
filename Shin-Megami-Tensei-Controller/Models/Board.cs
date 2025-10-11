namespace Shin_Megami_Tensei
{
    public class Board
    {
        public Dictionary<string, UnitBase?> PlayerOneBoard { get; }
        public Dictionary<string, UnitBase?> PlayerTwoBoard { get; }

        public List<UnitBase> PlayerOneRoster { get; }
        public List<UnitBase> PlayerTwoRoster { get; }
        
        internal readonly Dictionary<int, int> SkillUseCounters = new();
        
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
            for (var index = 0; index < GameConstants.BoardPositions.Length; index++)
            {
                var position = GameConstants.BoardPositions[index];
                board[position] = index < teamUnits.Count ? teamUnits[index] : null;
            }
            return board;
        }
    }
}