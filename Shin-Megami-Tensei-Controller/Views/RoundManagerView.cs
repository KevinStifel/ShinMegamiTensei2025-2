using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class RoundManagerView : AbstractView
{
    public RoundManagerView(View view) : base(view) { }

    public void ShowRoundHeader(int playerId, UnitBase teamLeaderUnit)
    {
        ShowSeparator();
        View.WriteLine($"Ronda de {teamLeaderUnit.Name} (J{playerId})");
    }

    public void ShowBothTeams(BoardManager board)
    {
        ShowSeparator();
        PrintPlayerBoard(board, 1, GetLeaderName(board, 1));
        PrintPlayerBoard(board, 2, GetLeaderName(board, 2));
    }

    private void PrintPlayerBoard(BoardManager board, int playerId, string samuraiName)
    {
        View.WriteLine($"Equipo de {samuraiName} (J{playerId})");
        foreach (var position in GameConstants.BoardPositions)
        {
            var unit = board.GetBoardForPlayer(playerId)[position];
            PrintUnitAtPosition(position, unit);
        }
    }

    private string GetLeaderName(BoardManager board, int playerId)
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
        => View.WriteLine($"{position}-");

    private void PrintOccupiedPosition(string position, UnitBase unit)
        => View.WriteLine($"{position}-{unit.Name} HP:{unit.Stats.HP}/{unit.Stats.MaxHP} MP:{unit.Stats.MP}/{unit.Stats.MaxMP}");

    public void ShowTurnStatus(int full, int blinking)
    {
        ShowSeparator();
        View.WriteLine($"Full Turns: {full}");
        View.WriteLine($"Blinking Turns: {blinking}");
    }

    public void ShowAttackOrder(IReadOnlyList<UnitBase> attackOrder)
    {
        ShowSeparator();
        View.WriteLine("Orden:");
        for (int i = 0; i < attackOrder.Count; i++)
            View.WriteLine($"{i + 1}-{attackOrder[i].Name}");
    }
    public void ShowAvailableActionsForSamurai(UnitBase unit)
    {
        ShowSeparator();
        View.WriteLine($"Seleccione una acción para {unit.Name}");
        View.WriteLine("1: Atacar");
        View.WriteLine("2: Disparar");
        View.WriteLine("3: Usar Habilidad");
        View.WriteLine("4: Invocar");
        View.WriteLine("5: Pasar Turno");
        View.WriteLine("6: Rendirse");
    }
    public void ShowAvailableActionsForMonster(UnitBase unit)
    {
        ShowSeparator();
        View.WriteLine($"Seleccione una acción para {unit.Name}");
        View.WriteLine("1: Atacar");
        View.WriteLine("2: Usar Habilidad");
        View.WriteLine("3: Invocar");
        View.WriteLine("4: Pasar Turno");
    }
    
    public void ShowWinner(BattleOutcome outcome, BoardManager board)
    {
        ShowSeparator();
        int winnerId = (int)outcome;
        var leader = board.GetTeamLeaderUnit(winnerId);
        View.WriteLine($"Ganador: {leader.Name} (J{winnerId})");
    }

    private void ShowSeparator()
        => View.WriteLine("----------------------------------------");
}
