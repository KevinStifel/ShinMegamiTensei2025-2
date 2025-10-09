using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DeadAllySelector : TargetSelectorBase
{
    public DeadAllySelector(View view, BoardManager board)
        : base(view, board, new DeadAllySelectorView(view))
    {
    }

    public override UnitBase? SelectTarget(UnitBase caster, int currentPlayerId)
    {
        List<UnitBase> deadAllies = Board.GetDeadUnits(currentPlayerId);
        SelectorView.ShowAvailableTargets(caster, deadAllies);

        int index = ReadTargetIndex(deadAllies);
        return WasCanceledSelection(index) ? null : deadAllies[index];
    }
}