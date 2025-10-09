using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class AllySelector : TargetSelectorBase
{
    private readonly AllySelectorView _selectorView;

    public AllySelector(BoardManager board, AllySelectorView view)
        : base(view, board)
    {
        _selectorView = view;
    }

    public override UnitBase? SelectTarget(UnitBase caster, int currentPlayerId)
    {
        List<UnitBase> allies = Board.GetAliveUnits(currentPlayerId);
        _selectorView.ShowAvailableTargets(caster, allies);

        int index = ReadTargetIndex(allies);
        return WasCanceledSelection(index) ? null : allies[index];
    }
}