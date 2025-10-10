using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class BattleContext
{
    public int CurrentPlayerId { get; }
    public BoardManager BoardManager { get; }
    public TurnManager TurnManager { get; }
    public View View { get; }

    public BattleContext(int currentPlayerId, BoardManager boardManager, TurnManager turnManager, View view)
    {
        CurrentPlayerId = currentPlayerId;
        BoardManager = boardManager;
        TurnManager = turnManager;
        View = view;
    }
}