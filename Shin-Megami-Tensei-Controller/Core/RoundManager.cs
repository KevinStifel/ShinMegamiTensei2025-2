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
        var activeUnits = board.GetAliveUnits(playerId);
        _turnManager.StartNewRound(activeUnits);

        var teamLeaderUnit = board.GetTeamLeaderUnit(playerId);
        ShowRoundResume(playerId, teamLeaderUnit, board);
    }

    private void ShowRoundResume(int playerId, UnitBase teamLeaderUnit, Board board)
    {
        _roundView.ShowRound(playerId, teamLeaderUnit, board);
        _roundView.ShowTurnStatus(_turnManager.FullTurns, _turnManager.BlinkingTurns);
        _roundView.ShowAttackOrder(_turnManager.AttackOrder);
    }
}