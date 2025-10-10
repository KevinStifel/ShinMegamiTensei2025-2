using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SurrenderAction : CombatActionBase
{
    public SurrenderAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var teamLeader = boardManager.GetTeamLeaderUnit(currentPlayerId);
        ActionView.ShowSurrender(teamLeader, currentPlayerId);

        foreach (var unit in boardManager.GetBoardForPlayer(currentPlayerId).Values)
        {
            if (unit == null) continue;
            if (unit.Stats.HP > 0)
                unit.Stats.TakeDamage(unit.Stats.HP);
        }

        if (turnManager is { FullTurns: <= 0, BlinkingTurns: <= 0 })
            return;

        var turnChange = new TurnChange(turnManager.FullTurns, turnManager.BlinkingTurns, 0);
        turnManager.ApplyTurnChange(turnChange);
    }
}