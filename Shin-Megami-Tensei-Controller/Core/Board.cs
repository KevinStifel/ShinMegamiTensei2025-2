namespace Shin_Megami_Tensei;

public class Board
{
    private readonly Dictionary<string, UnitBase?> _playerOneBoard;
    private readonly Dictionary<string, UnitBase?> _playerTwoBoard;
    private readonly List<UnitBase> _playerOneReserve;
    private readonly List<UnitBase> _playerTwoReserve;

    public Board(List<UnitBase> playerOneUnits, List<UnitBase> playerTwoUnits)
    {
        _playerOneBoard = InitializeBoard(playerOneUnits, out _playerOneReserve);
        _playerTwoBoard = InitializeBoard(playerTwoUnits, out _playerTwoReserve);
    }

    private Dictionary<string, UnitBase?> InitializeBoard(List<UnitBase> teamUnits, out List<UnitBase> reserveUnits)
    {
        var board = AssignActiveUnits(teamUnits);
        reserveUnits = ExtractReserveUnits(teamUnits);
        return board;
    }

    private Dictionary<string, UnitBase?> AssignActiveUnits(List<UnitBase> teamUnits)
    {
        var board = new Dictionary<string, UnitBase?>();

        for (int i = 0; i < GameConstants.BoardPositions.Length; i++)
        {
            string position = GameConstants.BoardPositions[i];
            board[position] = i < teamUnits.Count ? teamUnits[i] : null;
        }
        return board;
    }

    private List<UnitBase> ExtractReserveUnits(List<UnitBase> teamUnits)
    {
        return teamUnits.Count > GameConstants.BoardPositions.Length
            ? teamUnits.Skip(GameConstants.BoardPositions.Length).ToList()
            : new List<UnitBase>();
    }

    public IReadOnlyDictionary<string, UnitBase?> GetBoardForPlayer(int playerId)
    {
        return playerId == 1 ? _playerOneBoard : _playerTwoBoard;
    }

    public UnitBase GetTeamLeaderUnit(int playerId)
    {
        return GetBoardForPlayer(playerId)[GameConstants.BoardPositions[0]]!;
    }

    public List<UnitBase> GetReserveForPlayer(int playerId)
    {
        return playerId == 1 ? _playerOneReserve : _playerTwoReserve;
    }
    public List<UnitBase> GetAliveUnits(int playerId)
    {
        return GetBoardForPlayer(playerId)
            .Values
            .Where(unit => unit != null && unit.Stats.HP > 0)
            .Cast<UnitBase>()
            .ToList();
    }

}