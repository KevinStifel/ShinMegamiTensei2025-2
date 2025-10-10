using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class ReserveSelectorAll : TargetSelectorBase
{
    public ReserveSelectorAll(View view, BoardManager boardManager)
        : base(view, boardManager, new SpecialSelectorView(view)) { }

    public override List<UnitBase> SelectTargets(UnitBase caster, int currentPlayerId, SkillData skillData)
    {
        // ✅ Mostrar todas las unidades de la reserva (vivas y muertas)
        var reserveUnits = Board.GetReserveUnitsForPlayer(currentPlayerId);
        SelectorView.ShowAvailableTargets(caster, reserveUnits);

        int monsterIndex = ReadTargetIndex(reserveUnits);
        if (WasCanceledSelection(monsterIndex))
            throw new ActionCanceledException();

        var monsterToSummon = reserveUnits[monsterIndex];
        SelectorView.ShowSeparator();

        var playerBoard = Board.SelectMutableBoard(currentPlayerId);
        var summonOptions = GameConstants.BoardPositions
            .Skip(1) 
            .Select(pos => (Position: pos, Occupant: playerBoard[pos]))
            .ToList();

        ((SpecialSelectorView)SelectorView).ShowSummonPositions(summonOptions);

        int posIndex = ((SpecialSelectorView)SelectorView).ReadPositionIndex(summonOptions.Count);
        if (WasCanceledSelection(posIndex))
            throw new ActionCanceledException();

        var (chosenPosition, occupant) = summonOptions[posIndex];

        // ✅ Guardar decisión para el efecto
        Board.PrepareSummonData(currentPlayerId, monsterToSummon, chosenPosition, occupant);

        return new List<UnitBase> { monsterToSummon };
    }
}