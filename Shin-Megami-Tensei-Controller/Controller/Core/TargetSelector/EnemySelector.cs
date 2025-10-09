using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class EnemySelector : TargetSelectorBase
{
    public EnemySelector(View view, BoardManager board)
        : base(view, board, new EnemySelectorView(view))
    {
    }

    public override UnitBase? SelectTarget(UnitBase caster, int currentPlayerId)
    {
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        List<UnitBase> enemies = Board.GetAliveUnits(enemyPlayerId);

        SelectorView.ShowAvailableTargets(caster, enemies);

        int index = ReadTargetIndex(enemies);
        return WasCanceledSelection(index) ? null : enemies[index];
    }
}