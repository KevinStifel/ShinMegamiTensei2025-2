using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonAction : CombatActionBase
{
    public SummonAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager boardManager, TurnManager turnManager)
    {
        var summoner = turnManager.GetAttackerOnTurn();
        var summonEffect = new SummonEffect(View);

        var reserveUnits = boardManager.GetAliveReserveUnitsForPlayer(currentPlayerId);
        var monsterToSummon = SelectMonsterFromReserve(reserveUnits);
        if (monsterToSummon == null)
            throw new ActionCanceledException();

        var playerBoard = boardManager.SelectMutableBoard(currentPlayerId);
        UnitBase? replacedUnit;

        if (summoner is Samurai)
        {
            var summonOptions = GameConstants.BoardPositions.Skip(1)
                .Select(pos => (Position: pos, Occupant: playerBoard[pos]))
                .ToList();

            int chosenIndex = ActionView.ReadSummonPositionIndex(summonOptions);
            if (WasCanceledSelection(chosenIndex))
                throw new ActionCanceledException();

            var (chosenPosition, occupant) = summonOptions[chosenIndex];
            replacedUnit = summonEffect.ApplySamuraiSummon(monsterToSummon, chosenPosition, occupant, playerBoard, reserveUnits);
        }
        else
        {
            replacedUnit = summonEffect.ApplyMonsterSummon(summoner, monsterToSummon, playerBoard, reserveUnits);
        }

        turnManager.UpdateOrderAfterSummon(summoner, monsterToSummon, replacedUnit);

        var delta = turnManager.ConsumeSummonTurn();
        ActionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }

    private UnitBase? SelectMonsterFromReserve(List<UnitBase> reserveUnits)
    {
        int selectedIndex = ActionView.ReadSummonIndex(reserveUnits);
        if (WasCanceledSelection(selectedIndex))
            return null;
        return reserveUnits[selectedIndex];
    }
}