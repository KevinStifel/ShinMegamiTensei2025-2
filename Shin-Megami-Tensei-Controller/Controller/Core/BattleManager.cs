using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class BattleManager
{
    private readonly RoundManager _roundManager;
    private BoardManager _boardManager;

    public BattleManager(Board board, View view)
    {
        _boardManager = new BoardManager(board);
        _roundManager = new RoundManager(view);
        
        _boardManager.ResetPlayerSkillCounter(1);
        _boardManager.ResetPlayerSkillCounter(2);
    }
    public void StartBattle()
    {
        int currentPlayerId = 1;

        while (true)
        {
            try
            {
                _roundManager.StartNewRound(currentPlayerId, _boardManager);
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