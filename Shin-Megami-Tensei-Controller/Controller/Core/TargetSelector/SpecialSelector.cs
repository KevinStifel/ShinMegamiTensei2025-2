using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialSelector : TargetSelectorBase
{
    public SpecialSelector(View view, BoardManager boardManager)
        : base(view, boardManager, new SpecialSelectorView(view)) { }

    public override List<UnitBase> SelectTargets(UnitBase caster, int currentPlayerId, SkillData skillData)
    {
        // 1️⃣ Mostrar monstruos vivos en la reserva
        List<UnitBase> reserveUnits = Board.GetAliveReserveUnitsForPlayer(currentPlayerId);
        SelectorView.ShowAvailableTargets(caster, reserveUnits);

        int monsterIndex = ReadTargetIndex(reserveUnits);
        if (WasCanceledSelection(monsterIndex))
            throw new ActionCanceledException();

        var monsterToSummon = reserveUnits[monsterIndex];
        View.WriteLine("----------------------------------------");

        // 2️⃣ Mostrar posiciones del tablero para invocar
        var playerBoard = Board.SelectMutableBoard(currentPlayerId);
        var summonOptions = GameConstants.BoardPositions
            .Skip(1) // ignorar al Samurai
            .Select(pos => (Position: pos, Occupant: playerBoard[pos]))
            .ToList();

        ((SpecialSelectorView)SelectorView).ShowSummonPositions(summonOptions);

        int posIndex = ((SpecialSelectorView)SelectorView).ReadPositionIndex(summonOptions.Count);
        if (WasCanceledSelection(posIndex))
            throw new ActionCanceledException();

        var (chosenPosition, occupant) = summonOptions[posIndex];

        // Guardamos temporalmente la decisión para el efecto
        Board.PrepareSummonData(currentPlayerId, monsterToSummon, chosenPosition, occupant);

        return [monsterToSummon];
    }
}