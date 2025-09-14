using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class RoundManagerView
{
    private readonly View _view;

    public RoundManagerView(View view)
    {
        _view = view;
    }

    public void ShowRound(int playerId, UnitBase teamLeaderUnit, Board board)
    {
        ShowSeparator();
        _view.WriteLine($"Ronda de {teamLeaderUnit.Name} (J{playerId})");
        ShowSeparator();

        PrintPlayerBoard(board, playerId: 1, samuraiName: GetLeaderName(board, 1));
        PrintPlayerBoard(board, playerId: 2, samuraiName: GetLeaderName(board, 2));
    }
    
    private void PrintPlayerBoard(Board board, int playerId, string samuraiName)
    {
        _view.WriteLine($"Equipo de {samuraiName} (J{playerId})");

        foreach (var position in GameConstants.BoardPositions)
        {
            var unit = board.GetBoardForPlayer(playerId)[position];
            PrintUnitAtPosition(position, unit);
        }
    }

    private string GetLeaderName(Board board, int playerId)
    {
        var playerBoard = board.GetBoardForPlayer(playerId);
        var leaderPosition = GameConstants.BoardPositions[0]; // "A"
        var leader = playerBoard[leaderPosition];
        return leader!.Name;
    }


    private void PrintUnitAtPosition(string position, UnitBase? unit)
    {
        if (unit == null)
            PrintEmptyPosition(position);
        else
            PrintOccupiedPosition(position, unit);
    }

    private void PrintEmptyPosition(string position)
    {
        _view.WriteLine($"{position}-");
    }

    private void PrintOccupiedPosition(string position, UnitBase unit)
    {
        _view.WriteLine(
            $"{position}-{unit.Name} HP:{unit.Stats.HP}/{unit.Stats.MaxHP} MP:{unit.Stats.MP}/{unit.Stats.MaxMP}"
        );
    }
    public void ShowTurnStatus(int full, int blinking)
    {
        ShowSeparator();
        _view.WriteLine($"Full Turns: {full}");
        _view.WriteLine($"Blinking Turns: {blinking}");
    }
    
    public void ShowAttackOrder( IReadOnlyList<UnitBase> attackOrder)
    {
        ShowSeparator();
        _view.WriteLine("Orden:");

        for (int i = 0; i < attackOrder.Count; i++)
        {
            var unit = attackOrder[i];
            _view.WriteLine($"{i + 1}-{unit.Name}");
        }
    }


    private void ShowSeparator()
    {
        _view.WriteLine("----------------------------------------");
    }
}