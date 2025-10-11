using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialSelector : TargetSelectorBase
{
    public SpecialSelector(View view, BoardManager boardManager)
        : base(view, boardManager, new SpecialSelectorView(view)) { }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        List<UnitBase> reserveUnits = Board.GetAliveReserveUnitsForPlayer(currentPlayerId);
        SelectorView.ShowAvailableTargets(activeUnit, reserveUnits);

        int monsterIndex = ReadTargetIndex(reserveUnits);
        if (IsSelectionCanceled(monsterIndex))
            throw new ActionCanceledException();

        var monsterToSummon = reserveUnits[monsterIndex];
        SelectorView.ShowSeparator();

        var playerBoard = Board.SelectMutableBoard(currentPlayerId);
        var summonOptions = GameConstants.BoardPositions
            .Skip(1)
            .Select(position => (Position: position, Occupant: playerBoard[position]))
            .ToList();

        ((SpecialSelectorView)SelectorView).ShowSummonPositions(summonOptions);

        int positionIndex = ((SpecialSelectorView)SelectorView).ReadPositionIndex(summonOptions.Count);
        if (IsSelectionCanceled(positionIndex))
            throw new ActionCanceledException();

        var (chosenPosition, occupant) = summonOptions[positionIndex];

        Board.PrepareSummonData(currentPlayerId, monsterToSummon, chosenPosition, occupant);

        return [monsterToSummon];
    }
}