using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class BattleManager
{
    private readonly Board _board;
    private readonly RoundManager _roundManager;
    private readonly BattleManagerView _battleView;

    public BattleManager(Board board, View view)
    {
        _board = board;
        _roundManager = new RoundManager(view);
        _battleView = new BattleManagerView(view);
    }
    public void StartBattle()
    {
        int currentPlayerId = 1;

        while (true)
        {
            try
            {
                _roundManager.StartNewRound(currentPlayerId, _board);
                currentPlayerId = SwitchCurrentPlayer(currentPlayerId);
            }
            catch (BattleEndedException)
            {
                return;
            }
        }
    }
    private static int SwitchCurrentPlayer(int currentPlayerId)
        => currentPlayerId == 1 ? 2 : 1;
}