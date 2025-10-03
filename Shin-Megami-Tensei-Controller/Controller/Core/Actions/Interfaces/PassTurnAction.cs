using Shin_Megami_Tensei_View;
namespace Shin_Megami_Tensei;

public sealed class PassTurnAction : CombatActionBase
{
    public PassTurnAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, Board board, TurnManager turnManager)
    {
        var delta = turnManager.ConsumePassTurn();
        _actionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }
}