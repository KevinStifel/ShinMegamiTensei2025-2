using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DeadAllySelector : TargetSelectorBase
{
    public DeadAllySelector(View view, BoardManager board)
        : base(view, board, new DeadAllySelectorView(view))
    {
    }

    public override List<UnitBase> SelectTargets(UnitBase caster, int currentPlayerId, SkillData skillData)
    {
        List<UnitBase> deadAllies = Board.GetDeadUnits(currentPlayerId);
        SelectorView.ShowAvailableTargets(caster, deadAllies);
        int index = ReadTargetIndex(deadAllies);
        if (WasCanceledSelection(index))
            return [];
        return [deadAllies[index]];
    }
}