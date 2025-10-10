using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class BattleFlowContext
{
    public int CurrentPlayerId { get; }
    public BoardManager BoardManager { get; }
    public TurnManager TurnManager { get; }
    public View View { get; }

    public BattleFlowContext(int currentPlayerId, BoardManager boardManager, TurnManager turnManager, View view)
    {
        CurrentPlayerId = currentPlayerId;
        BoardManager = boardManager;
        TurnManager = turnManager;
        View = view;
    }
}