using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class TargetSelectorBase
{
    protected readonly TargetSelectorViewBase SelectorView;
    protected readonly BoardManager Board;

    protected TargetSelectorBase(TargetSelectorViewBase selectorView, BoardManager board)
    {
        SelectorView = selectorView;
        Board = board;
    }

    public abstract UnitBase? SelectTarget(UnitBase caster, int currentPlayerId);

    protected int ReadTargetIndex(List<UnitBase> candidates)
        => SelectorView.ReadTargetIndex(candidates.Count);

    protected static bool WasCanceledSelection(int index)
        => index < 0;
}