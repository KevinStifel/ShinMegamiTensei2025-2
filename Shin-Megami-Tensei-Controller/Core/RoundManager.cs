using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class RoundManager
{
    private readonly RoundManagerView _roundView;
    private readonly TurnManager _turnManager;

    public RoundManager(View view)
    {
        _roundView = new RoundManagerView(view);
        _turnManager = new TurnManager();
    }

    public void StartNewRound(int playerId, Board board)
    {
        _turnManager.StartNewRound();

        var samurai = (Samurai)board.GetBoardForPlayer(playerId)[GameConstants.BoardPositions[0]]!;
        _roundView.ShowRound(playerId, samurai.Name, board);

        // 🔹 Aquí después se integrará el ciclo de turnos y acciones
    }
}