using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class AllySelector : TargetSelectorBase
{
    public AllySelector(View view, BoardManager board)
        : base(view, board) { }

    protected override List<UnitBase> GetCandidates(UnitBase caster, int currentPlayerId)
    {
        return Board.GetAliveUnits(currentPlayerId);
    }
}