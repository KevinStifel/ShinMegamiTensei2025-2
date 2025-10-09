using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DeadAllySelector : TargetSelectorBase
{
    private readonly DeadAllySelectorView _selectorView;

    public DeadAllySelector(BoardManager board, DeadAllySelectorView view)
        : base(view, board)
    {
        _selectorView = view;
    }

    public override UnitBase? SelectTarget(UnitBase caster, int currentPlayerId)
    {
        List<UnitBase> deadAllies = Board.GetDeadUnits(currentPlayerId);
        _selectorView.ShowAvailableTargets(caster, deadAllies);

        int index = ReadTargetIndex(deadAllies);
        return WasCanceledSelection(index) ? null : deadAllies[index];
    }
}