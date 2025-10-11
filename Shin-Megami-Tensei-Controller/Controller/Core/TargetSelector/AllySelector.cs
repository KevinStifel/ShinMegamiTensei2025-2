using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class AllySelector : TargetSelectorBase
{
    public AllySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new AllySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        List<UnitBase> allies = Board.GetAliveUnits(currentPlayerId);
        SelectorView.ShowAvailableTargets(activeUnit, allies);
        int index = ReadTargetIndex(allies);
        if (IsSelectionCanceled(index))
            return [];
        return [allies[index]];
    }
}