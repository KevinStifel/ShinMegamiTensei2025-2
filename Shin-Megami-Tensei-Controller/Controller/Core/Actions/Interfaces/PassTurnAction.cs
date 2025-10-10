using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class PassTurnAction : CombatActionBase
{
    public PassTurnAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        TurnChange turnChange = turnManager.ConsumePassTurn();
        ActionView.ShowTurnConsumption(turnChange);
    }
}