using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class AllySelector : TargetSelectorBase
{
    public AllySelector(View view, BoardManager board)
        : base(view, board, new AllySelectorView(view))
    {
    }

    public override UnitBase? SelectTarget(UnitBase caster, int currentPlayerId)
    {
        List<UnitBase> allies = Board.GetAliveUnits(currentPlayerId);
        SelectorView.ShowAvailableTargets(caster, allies);

        int index = ReadTargetIndex(allies);
        return WasCanceledSelection(index) ? null : allies[index];
    }
}