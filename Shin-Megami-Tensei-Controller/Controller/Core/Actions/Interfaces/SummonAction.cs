using Shin_Megami_Tensei_View;
using Shin_Megami_Tensei;

public sealed class SummonAction : CombatActionBase
{
    public SummonAction(View view) : base(view) { }

    public override void ExecuteAction(int currentPlayerId, BoardManager board, TurnManager turnManager)
    {
        var summoner = turnManager.GetAttackerOnTurn();
        var reserveUnits = board.GetAliveReserveUnitsForPlayer(currentPlayerId);

        var monsterToSummon = SelectMonsterFromReserve(reserveUnits);
        if (monsterToSummon == null)
            throw new ActionCanceledException();

        var playerBoard = board.SelectMutableBoard(currentPlayerId);
        if (summoner is Samurai)
        {
            var replacedUnit = SummonBySamurai(playerBoard, reserveUnits, monsterToSummon);
            turnManager.UpdateOrderAfterSummon(summoner, monsterToSummon, replacedUnit);
        }
        else
        {
            var replacedUnit = SummonByMonster(playerBoard, reserveUnits, summoner, monsterToSummon);
            turnManager.UpdateOrderAfterSummon(summoner, monsterToSummon, replacedUnit);
        }
        ActionView.ShowSummonResult(monsterToSummon);

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

    private UnitBase? SummonBySamurai(Dictionary<string, UnitBase?> playerBoard, List<UnitBase> reserveUnits, UnitBase monsterToSummon)
    {
        var summonOptions = GameConstants.BoardPositions
            .Skip(1) // no incluir al Samurai
            .Select(pos => (Position: pos, Occupant: playerBoard[pos]))
            .ToList();

        int chosenIndex = ActionView.ReadSummonPositionIndex(summonOptions);
        if (WasCanceledSelection(chosenIndex))
            throw new ActionCanceledException();

        var (chosenPosition, occupantAtPosition) = summonOptions[chosenIndex];

        // colocar el nuevo monstruo
        playerBoard[chosenPosition] = monsterToSummon;
        reserveUnits.Remove(monsterToSummon);

        // si había un monstruo en ese puesto → va al INICIO de la reserva
        if (occupantAtPosition != null)
            reserveUnits.Insert(0, occupantAtPosition);

        return occupantAtPosition; // para actualizar orden
    }

// 🔹 Flujo Monstruo: se reemplaza a sí mismo
    private UnitBase SummonByMonster(Dictionary<string, UnitBase?> playerBoard, List<UnitBase> reserveUnits, UnitBase summoner, UnitBase monsterToSummon)
    {
        var summonerPosition = playerBoard.First(kvp => ReferenceEquals(kvp.Value, summoner)).Key;

        // reemplazar directamente
        playerBoard[summonerPosition] = monsterToSummon;

        // sacar al invocado de la reserva
        reserveUnits.Remove(monsterToSummon);

        // el invocador sale del tablero → va al INICIO de la reserva
        reserveUnits.Insert(0, summoner);

        return summoner; // para actualizar orden
    }

}
