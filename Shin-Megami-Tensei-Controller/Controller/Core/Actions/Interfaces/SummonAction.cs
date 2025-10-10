using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonAction : CombatActionBase
{
    public SummonAction(View view) : base(view) { }

    public override void ExecuteAction(BattleFlowContext battleFlowContext)
    {
        var boardManager = battleFlowContext.BoardManager;
        var turnManager = battleFlowContext.TurnManager;
        var view = battleFlowContext.View;
        var currentPlayerId = battleFlowContext.CurrentPlayerId;

        var summonerUnit = turnManager.GetAttackerOnTurn();
        var summonEffect = new SummonEffect(view);

        var monsterToSummon = SelectMonsterToSummon(boardManager, currentPlayerId);
        if (monsterToSummon == null)
            throw new ActionCanceledException();

        var boardFormation = CreateBoardFormation(boardManager, currentPlayerId);
        var summonData = new SummonData(summonerUnit, monsterToSummon);

        var replacedUnit = PerformSummon(summonData, summonEffect, boardFormation);
        UpdateTurnAndOrder(turnManager, summonData, replacedUnit);
    }

    private UnitBase? SelectMonsterToSummon(BoardManager boardManager, int currentPlayerId)
    {
        var availableReserveUnits = boardManager.GetAliveReserveUnitsForPlayer(currentPlayerId);
        int selectedIndex = ActionView.ReadSummonIndex(availableReserveUnits);
        if (WasCanceledSelection(selectedIndex)) return null;
        return availableReserveUnits[selectedIndex];
    }

    private static PlayerBoardFormation CreateBoardFormation(BoardManager boardManager, int currentPlayerId)
    {
        var activeBoard = boardManager.SelectMutableBoard(currentPlayerId);
        var reserveUnits = boardManager.GetAliveReserveUnitsForPlayer(currentPlayerId);
        return new PlayerBoardFormation(activeBoard, reserveUnits);
    }

    private UnitBase? PerformSummon(SummonData summonData, SummonEffect summonEffect, PlayerBoardFormation boardFormation)
    {
        return summonData.Summoner is Samurai
            ? SummonWithSamurai(summonData, summonEffect, boardFormation)
            : SummonWithMonster(summonData, summonEffect, boardFormation);
    }

    private UnitBase? SummonWithSamurai(SummonData summonData, SummonEffect summonEffect, PlayerBoardFormation boardFormation)
    {
        var summonOptions = GetSummonPositions(boardFormation.ActiveBoard);
        int selectedIndex = ActionView.ReadSummonPositionIndex(summonOptions);
        if (WasCanceledSelection(selectedIndex))
            throw new ActionCanceledException();

        var (boardPosition, replacedUnit) = summonOptions[selectedIndex];
        var placement = new SummonPlacement(boardPosition, replacedUnit);

        return summonEffect.ApplySamuraiSummon(summonData.MonsterToSummon, boardFormation, placement);
    }

    private UnitBase? SummonWithMonster(SummonData summonData, SummonEffect summonEffect, PlayerBoardFormation boardFormation)
    {
        return summonEffect.ApplyMonsterSummon(summonData, boardFormation);
    }

    private static List<(string BoardPosition, UnitBase? ReplacedUnit)> GetSummonPositions(Dictionary<string, UnitBase?> playerBoard)
    {
        return GameConstants.BoardPositions
            .Skip(1)
            .Select(position => (BoardPosition: position, ReplacedUnit: playerBoard[position]))
            .ToList();
    }

    private void UpdateTurnAndOrder(TurnManager turnManager, SummonData summonData, UnitBase? replacedUnit)
    {
        turnManager.UpdateOrderAfterSummon(
            summonData.Summoner,
            summonData.MonsterToSummon,
            replacedUnit);

        var turnChange = turnManager.ConsumeSummonTurn();
        ActionView.ShowTurnConsumption(turnChange);
    }
}
