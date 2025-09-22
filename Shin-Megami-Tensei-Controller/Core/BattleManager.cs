using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class BattleManager
{
    private readonly Board _board;
    private readonly View _view;
    private readonly RoundManager _roundManager;

    public BattleManager(Board board, List<UnitBase> playerOneUnitList, List<UnitBase> playerTwoUnitList, View view)
    {
        _board = board;
        _view = view;
        _roundManager = new RoundManager(view);
    }

    public void StartBattle()
    {
        int currentPlayerId = 1;

        while (true)
        {
            // 🔹 Condición de victoria antes de iniciar la ronda
            if (IsGameOver(out int winnerId))
            {
                AnnounceWinner(winnerId);
                break;
            }

            _roundManager.StartNewRound(currentPlayerId, _board);

            // 🔹 Cambiar jugador
            currentPlayerId = currentPlayerId == 1 ? 2 : 1;
        }
    }

    private bool IsGameOver(out int winnerId)
    {
        bool playerOneAlive = _board.GetAliveUnits(1).Any();
        bool playerTwoAlive = _board.GetAliveUnits(2).Any();

        if (!playerOneAlive && !playerTwoAlive)
        {
            // Empate (opcional)
            winnerId = 0;
            return true;
        }
        if (!playerOneAlive)
        {
            winnerId = 2;
            return true;
        }
        if (!playerTwoAlive)
        {
            winnerId = 1;
            return true;
        }

        winnerId = -1;
        return false;
    }

    private void AnnounceWinner(int winnerId)
    {
        _view.WriteLine("----------------------------------------");

        if (winnerId == 0)
            _view.WriteLine("Empate: ambos equipos fueron derrotados.");
        else
        {
            var leader = _board.GetTeamLeaderUnit(winnerId);
            _view.WriteLine($"Ganador: {leader.Name} (J{winnerId})");
        }
    }
}