using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class EnemySelector : TargetSelectorBase
{
    public EnemySelector(View view, BoardManager board)
        : base(view, board) { }

    protected override List<UnitBase> GetCandidates(UnitBase caster, int currentPlayerId)
    {
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        return Board.GetAliveUnits(enemyPlayerId);
    }
}
