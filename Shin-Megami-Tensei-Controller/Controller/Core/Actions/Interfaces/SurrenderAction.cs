using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei;

public sealed class SurrenderAction : CombatActionBase
{
    public SurrenderAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager)
    {
        var teamLeader = board.GetTeamLeaderUnit(currentPlayerId);
        ActionView.ShowSurrender(teamLeader, currentPlayerId);

        foreach (var unit in board.GetBoardForPlayer(currentPlayerId).Values)
        {
            if (unit is null) continue;
            if (unit.Stats.HP > 0)
                unit.Stats.TakeDamage(unit.Stats.HP);
        }

        if (turnManager is { FullTurns: <= 0, BlinkingTurns: <= 0 }) return;
        var change = new TurnChange(turnManager.FullTurns, turnManager.BlinkingTurns, 0);
        turnManager.ApplyTurnChange(change);
    }
}