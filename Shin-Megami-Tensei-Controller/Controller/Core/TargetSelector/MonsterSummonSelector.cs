using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class MonsterSummonSelector : TargetSelectorBase
{
    public MonsterSummonSelector(View view, BoardManager board)
        : base(view, board, new SpecialSelectorView(view)) { }

    public override List<UnitBase> SelectTargets(UnitBase caster, int currentPlayerId, SkillData skillData)
    {
        // Muestra solo monstruos vivos en la reserva
        List<UnitBase> reserveUnits = Board.GetAliveReserveUnitsForPlayer(currentPlayerId);
        SelectorView.ShowAvailableTargets(caster, reserveUnits);

        int monsterIndex = ReadTargetIndex(reserveUnits);
        if (WasCanceledSelection(monsterIndex))
            throw new ActionCanceledException();

        var monsterToSummon = reserveUnits[monsterIndex];

        var playerBoard = Board.SelectMutableBoard(currentPlayerId);
        var summonerPosition = playerBoard.First(kvp => ReferenceEquals(kvp.Value, caster)).Key;

        Board.PrepareSummonData(currentPlayerId, monsterToSummon, summonerPosition, caster);

        return [monsterToSummon];
    }
}