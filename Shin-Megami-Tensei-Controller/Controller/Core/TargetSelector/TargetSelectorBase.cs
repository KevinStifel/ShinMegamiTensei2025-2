using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class TargetSelectorBase
{
    protected readonly TargetSelectorViewBase SelectorView;
    protected readonly BoardManager Board;
    protected readonly View View;

    protected TargetSelectorBase(View view, BoardManager boardManager, TargetSelectorViewBase selectorView)
    {
        View = view;
        Board = boardManager;
        SelectorView = selectorView;
    }

    public abstract List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData);

    protected int ReadTargetIndex(List<UnitBase> candidates)
        => SelectorView.ReadTargetIndex(candidates.Count);

    protected static bool IsSelectionCanceled(int index)
        => index < 0;
}