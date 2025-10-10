using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SurrenderAction : CombatActionBase
{
    public SurrenderAction(View view) : base(view) { }

    public override void ExecuteAction(BattleFlowContext battleFlowContext)
    {
        var boardManager = battleFlowContext.BoardManager;
        var teamLeader = boardManager.GetTeamLeaderUnit(battleFlowContext.CurrentPlayerId);

        ActionView.ShowSurrender(teamLeader, battleFlowContext.CurrentPlayerId);

        foreach (var unit in boardManager.GetBoardForPlayer(battleFlowContext.CurrentPlayerId).Values)
        {
            if (unit is null) continue;
            if (unit.Stats.HP > 0)
                unit.Stats.TakeDamage(unit.Stats.HP);
        }

        if (battleFlowContext.TurnManager is { FullTurns: <= 0, BlinkingTurns: <= 0 }) return;

        var turnChange = new TurnChange(
            battleFlowContext.TurnManager.FullTurns,
            battleFlowContext.TurnManager.BlinkingTurns,
            0
        );

        battleFlowContext.TurnManager.ApplyTurnChange(turnChange);
    }
}