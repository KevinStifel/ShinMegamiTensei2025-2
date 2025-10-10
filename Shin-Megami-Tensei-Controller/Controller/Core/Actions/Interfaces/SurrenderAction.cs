using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SurrenderAction : CombatActionBase
{
    public SurrenderAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var teamLeader = boardManager.GetTeamLeaderUnit(currentPlayerId);
        ActionView.ShowSurrender(teamLeader, currentPlayerId);

        ApplyFullDamageToActiveUnits(boardManager, currentPlayerId);
        ConsumeAllRemainingTurns(turnManager);
    }

    private static void ApplyFullDamageToActiveUnits(BoardManager boardManager, int currentPlayerId)
    {
        var playerUnits = boardManager.GetBoardForPlayer(currentPlayerId).Values;

        foreach (var unit in playerUnits)
        {
            if (!HasValidUnit(unit))
                continue;

            if (IsUnitAlive(unit!))
                unit!.Stats.TakeDamage(unit.Stats.HP);
        }
    }

    private static bool HasValidUnit(UnitBase? unit)
    {
        return unit != null;
    }

    private static bool IsUnitAlive(UnitBase unit)
    {
        return unit.Stats.HP > 0;
    }

    private void ConsumeAllRemainingTurns(TurnManager turnManager)
    {
        if (!HasRemainingTurns(turnManager))
            return;

        var turnChange = new TurnChange(turnManager.FullTurns, turnManager.BlinkingTurns, 0);
        turnManager.ApplyTurnChange(turnChange);
    }

    private static bool HasRemainingTurns(TurnManager turnManager)
    {
        return turnManager.FullTurns > 0 || turnManager.BlinkingTurns > 0;
    }
}