using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class EnemySelector : TargetSelectorBase
{
    private readonly EnemySelectorView _selectorView;

    public EnemySelector(BoardManager board, EnemySelectorView view)
        : base(view, board)
    {
        _selectorView = view;
    }

    public override UnitBase? SelectTarget(UnitBase caster, int currentPlayerId)
    {
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        List<UnitBase> enemies = Board.GetAliveUnits(enemyPlayerId);

        _selectorView.ShowAvailableTargets(caster, enemies);

        int index = ReadTargetIndex(enemies);
        return WasCanceledSelection(index) ? null : enemies[index];
    }
}