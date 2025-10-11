using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DeadAllySelector : TargetSelectorBase
{
    public DeadAllySelector(View view, BoardManager boardManager)
        : base(view, boardManager, new DeadAllySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        List<UnitBase> deadAllies = Board.GetAllDeadUnits(currentPlayerId);
        SelectorView.ShowAvailableTargets(activeUnit, deadAllies);
        int targetIndex = ReadTargetIndex(deadAllies);
        if (IsSelectionCanceled(targetIndex))
            return [];
        return [deadAllies[targetIndex]];
    }
}