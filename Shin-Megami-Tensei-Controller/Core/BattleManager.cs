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
        int currentPlayerId = 1;

        while (true)
        {
            _roundManager.StartNewRound(currentPlayerId, _board);

            if (IsGameOver())
                return;

            currentPlayerId = SwitchPlayer(currentPlayerId);
        }
    }

    private static int SwitchPlayer(int currentPlayerId)
        => currentPlayerId == 1 ? 2 : 1;

    private bool TryAnnounceAndExitIfGameOver()
    {
        int winnerId = GetWinnerId();
        if (winnerId == -1) return false;
        return true;
    }

    private bool IsGameOver()
    {
        return GetWinnerId() != -1;
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