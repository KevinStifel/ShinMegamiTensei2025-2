using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonEffect : EffectBase
{
    public SummonEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        TurnManager turnManager,
        int currentPlayerId,
        BoardManager board)
    {
        var summoner = caster;
        var reserveUnits = board.GetAliveReserveUnitsForPlayer(currentPlayerId);
        var playerBoard = board.SelectMutableBoard(currentPlayerId);

        int selectedIndex = EffectView.ReadSummonIndex(reserveUnits);
        if (selectedIndex < 0 || selectedIndex >= reserveUnits.Count)
            throw new ActionCanceledException();

        var monsterToSummon = reserveUnits[selectedIndex];

        UnitBase? replacedUnit;
        if (summoner is Samurai)
            replacedUnit = SummonBySamurai(playerBoard, reserveUnits, monsterToSummon);
        else
            replacedUnit = SummonByMonster(playerBoard, reserveUnits, summoner, monsterToSummon);

        // 🔄 Actualizar orden de turnos
        turnManager.UpdateOrderAfterSummon(summoner, monsterToSummon, replacedUnit);

        // 💬 Mostrar resultado
        EffectView.ShowSummonResult(monsterToSummon);

        // ⏱️ Consumir turno correspondiente
        var delta = turnManager.ConsumeSummonTurn();
        new CombatActionView(View).ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }

    private UnitBase? SummonBySamurai(Dictionary<string, UnitBase?> playerBoard, List<UnitBase> reserveUnits, UnitBase monsterToSummon)
    {
        var summonOptions = GameConstants.BoardPositions
            .Skip(1) // no incluir al Samurai
            .Select(pos => (Position: pos, Occupant: playerBoard[pos]))
            .ToList();

        int chosenIndex = EffectView.ReadSummonPositionIndex(summonOptions);
        if (chosenIndex < 0 || chosenIndex >= summonOptions.Count)
            throw new ActionCanceledException();

        var (chosenPosition, occupantAtPosition) = summonOptions[chosenIndex];

        // colocar el nuevo monstruo
        playerBoard[chosenPosition] = monsterToSummon;
        reserveUnits.Remove(monsterToSummon);

        // si había un monstruo en ese puesto → va al INICIO de la reserva
        if (occupantAtPosition != null)
            reserveUnits.Insert(0, occupantAtPosition);

        return occupantAtPosition;
    }

    private UnitBase SummonByMonster(Dictionary<string, UnitBase?> playerBoard, List<UnitBase> reserveUnits, UnitBase summoner, UnitBase monsterToSummon)
    {
        var summonerPosition = playerBoard.First(kvp => ReferenceEquals(kvp.Value, summoner)).Key;

        // reemplazar directamente
        playerBoard[summonerPosition] = monsterToSummon;

        // sacar al invocado de la reserva
        reserveUnits.Remove(monsterToSummon);

        // el invocador sale del tablero → va al INICIO de la reserva
        reserveUnits.Insert(0, summoner);

        return summoner;
    }
}
