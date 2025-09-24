using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class BattleManager
{
    private readonly Board _board;
    private readonly RoundManager _roundManager;
    private readonly BattleManagerView _battleView;

    public BattleManager(Board board, List<UnitBase> playerOneUnitList, List<UnitBase> playerTwoUnitList, View view)
    {
        _board = board;
        _roundManager = new RoundManager(view);
        _battleView = new BattleManagerView(view);
    }

    public void StartBattle()
    {
        if (TryAnnounceAndExitIfGameOver())
            return;

        int currentPlayerId = 1;

        while (true)
        {
            RoundResult result = _roundManager.StartNewRound(currentPlayerId, _board);

            if (result.DidBattleEnd)
            {
                _battleView.ShowWinner(result.WinnerId, _board);
                return;
            }

            currentPlayerId = SwitchPlayer(currentPlayerId);
        }
    }

    private static int SwitchPlayer(int currentPlayerId)
        => currentPlayerId == 1 ? 2 : 1;

    private bool TryAnnounceAndExitIfGameOver()
    {
        int winnerId = GetWinnerId();
        if (winnerId == -1) return false;

        _battleView.ShowWinner(winnerId, _board);
        return true;
    }

    private int GetWinnerId()
    {
        bool playerOneAlive = _board.GetAliveUnits(1).Any();
        bool playerTwoAlive = _board.GetAliveUnits(2).Any();

        if (!playerOneAlive && !playerTwoAlive) return 0;
        if (!playerOneAlive) return 2;
        if (!playerTwoAlive) return 1;
        return -1;
    }
}