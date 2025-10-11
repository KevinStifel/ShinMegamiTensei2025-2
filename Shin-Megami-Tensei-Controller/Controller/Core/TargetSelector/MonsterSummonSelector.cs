using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class MonsterSummonSelector : TargetSelectorBase
{
    public MonsterSummonSelector(View view, BoardManager boardManager)
        : base(view, boardManager, new SpecialSelectorView(view)) { }

    public override List<UnitBase> SelectTargets(UnitBase activeUnit, int currentPlayerId, SkillData skillData)
    {
        List<UnitBase> availableMonsters = GetAvailableReserveMonsters(currentPlayerId);
        ShowSummonableMonsters(activeUnit, availableMonsters);

        UnitBase monsterToSummon = GetSelectedMonster(availableMonsters);
        PrepareSummonInformation(currentPlayerId, monsterToSummon, activeUnit);

        return [monsterToSummon];
    }

    private List<UnitBase> GetAvailableReserveMonsters(int currentPlayerId)
    {
        return Board.GetAliveReserveUnitsForPlayer(currentPlayerId);
    }

    private void ShowSummonableMonsters(UnitBase activeUnit, List<UnitBase> monsters)
    {
        SelectorView.ShowAvailableTargets(activeUnit, monsters);
    }

    private UnitBase GetSelectedMonster(List<UnitBase> monsters)
    {
        int selectedIndex = ReadTargetIndex(monsters);
        bool selectionWasCanceled = IsSelectionCanceled(selectedIndex);

        if (selectionWasCanceled)
            throw new ActionCanceledException();

        return monsters[selectedIndex];
    }

    private void PrepareSummonInformation(int currentPlayerId, UnitBase monsterToSummon, UnitBase activeUnit)
    {
        string summonerPosition = GetSummonerBoardPosition(currentPlayerId, activeUnit);
        Board.PrepareSummonData(currentPlayerId, monsterToSummon, summonerPosition, activeUnit);
    }

    private string GetSummonerBoardPosition(int currentPlayerId, UnitBase activeUnit)
    {
        var playerBoard = Board.SelectMutableBoard(currentPlayerId);
        var boardSlot = playerBoard.First(slot => ReferenceEquals(slot.Value, activeUnit));

        string positionKey = boardSlot.Key;
        return positionKey;
    }

}
