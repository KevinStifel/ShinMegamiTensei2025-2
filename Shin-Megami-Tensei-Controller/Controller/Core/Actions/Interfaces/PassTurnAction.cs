using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class PassTurnAction : CombatActionBase
{
    public PassTurnAction(View view) : base(view) { }

    public override void ExecuteAction(BattleFlowContext battleFlowContext)
    {
        var turnManager = battleFlowContext.TurnManager;
        TurnChange turnChange = turnManager.ConsumePassTurn();
        ActionView.ShowTurnConsumption(turnChange);
    }
}