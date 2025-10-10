using System.Collections.Generic;
using System.Linq;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonAction : CombatActionBase
{
    public SummonAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var summonerUnit = turnManager.GetAttackerOnTurn();
        var summonEffect = new SummonEffect(View);

        var monsterToSummon = SelectMonsterToSummon(boardManager, currentPlayerId);
        if (monsterToSummon == null)
            throw new ActionCanceledException();

        var replacedUnit = PerformSummon(summonerUnit, monsterToSummon, boardManager, currentPlayerId, summonEffect);
        UpdateTurnAndOrder(turnManager, summonerUnit, monsterToSummon, replacedUnit);
    }

    private UnitBase? SelectMonsterToSummon(BoardManager boardManager, int currentPlayerId)
    {
        var availableReserveUnits = boardManager.GetAliveReserveUnitsForPlayer(currentPlayerId);
        int selectedIndex = ActionView.ReadSummonIndex(availableReserveUnits);

        if (WasCanceledSelection(selectedIndex))
            return null;

        return availableReserveUnits[selectedIndex];
    }

    private UnitBase? PerformSummon(UnitBase summonerUnit, UnitBase monsterToSummon, BoardManager boardManager, int currentPlayerId, SummonEffect summonEffect)
    {
        var playerBoard = boardManager.SelectMutableBoard(currentPlayerId);
        var reserveUnits = boardManager.GetAliveReserveUnitsForPlayer(currentPlayerId);

        return summonerUnit is Samurai
            ? SummonWithSamurai(monsterToSummon, summonEffect, playerBoard, reserveUnits)
            : SummonWithMonster(summonerUnit, monsterToSummon, summonEffect, playerBoard, reserveUnits);
    }

    private UnitBase? SummonWithSamurai(UnitBase monsterToSummon, SummonEffect summonEffect, Dictionary<string, UnitBase?> playerBoard, List<UnitBase> reserveUnits)
    {
        var summonOptions = GetSummonPositions(playerBoard);

        int chosenIndex = ActionView.ReadSummonPositionIndex(summonOptions);
        if (WasCanceledSelection(chosenIndex))
            throw new ActionCanceledException();

        var (chosenPosition, currentOccupant) = summonOptions[chosenIndex];
        return summonEffect.ApplySamuraiSummon(monsterToSummon, chosenPosition, currentOccupant, playerBoard, reserveUnits);
    }

    private UnitBase? SummonWithMonster(UnitBase summonerUnit, UnitBase monsterToSummon, SummonEffect summonEffect, Dictionary<string, UnitBase?> playerBoard, List<UnitBase> reserveUnits)
    {
        return summonEffect.ApplyMonsterSummon(summonerUnit, monsterToSummon, playerBoard, reserveUnits);
    }

    private static List<(string Position, UnitBase? Occupant)> GetSummonPositions(Dictionary<string, UnitBase?> playerBoard)
    {
        return GameConstants.BoardPositions
            .Skip(1)
            .Select(position => (Position: position, Occupant: playerBoard[position]))
            .ToList();
    }

    private void UpdateTurnAndOrder(TurnManager turnManager, UnitBase summonerUnit, UnitBase summonedUnit, UnitBase? replacedUnit)
    {
        turnManager.UpdateOrderAfterSummon(summonerUnit, summonedUnit, replacedUnit);

        var turnChange = turnManager.ConsumeSummonTurn();
        ActionView.ShowTurnConsumption(turnChange.ConsumedFull, turnChange.ConsumedBlinking, turnChange.GainedBlinking);
    }
}
