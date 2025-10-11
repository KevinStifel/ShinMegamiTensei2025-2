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

    public void ShowBothTeams(BoardManager boardManager)
    {
        ShowSeparator();
        ShowPlayerBoard(boardManager, 1, GetLeaderName(boardManager, 1));
        ShowPlayerBoard(boardManager, 2, GetLeaderName(boardManager, 2));
    }

    private void ShowPlayerBoard(BoardManager boardManager, int playerId, string samuraiName)
    {
        View.WriteLine($"Equipo de {samuraiName} (J{playerId})");
        foreach (var position in GameConstants.BoardPositions)
        {
            var unit = boardManager.GetBoardForPlayer(playerId)[position];
            ShowUnitAtPosition(position, unit);
        }
    }

    private string GetLeaderName(BoardManager boardManager, int playerId)
    {
        var playerBoard = boardManager.GetBoardForPlayer(playerId);
        var leaderPosition = GameConstants.BoardPositions[0];
        var leader = playerBoard[leaderPosition];
        return leader!.Name;
    }

    private void ShowUnitAtPosition(string position, UnitBase? unit)
    {
        if (unit == null)
            ShowEmptyPosition(position);
        else
            ShowOccupiedPosition(position, unit);
    }

    private void ShowEmptyPosition(string position)
        => View.WriteLine($"{position}-");

    private void ShowOccupiedPosition(string position, UnitBase unit)
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
        for (int index = 0; index < attackOrder.Count; index++)
            View.WriteLine($"{index + 1}-{attackOrder[index].Name}");
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
    
    public void ShowWinner(BattleOutcome outcome, BoardManager boardManager)
    {
        ShowSeparator();
        int winnerId = (int)outcome;
        var leader = boardManager.GetTeamLeaderUnit(winnerId);
        View.WriteLine($"Ganador: {leader.Name} (J{winnerId})");
    }

}
