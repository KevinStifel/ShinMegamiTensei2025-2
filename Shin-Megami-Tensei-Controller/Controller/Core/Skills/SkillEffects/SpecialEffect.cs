using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        TurnManager turnManager,
        int currentPlayerId,
        BoardManager board)
    {
        var monsterToSummon = targets[0];
        EffectView.ShowSummon(monsterToSummon);

        // Obtener datos guardados por el selector
        var (position, replaced) = board.GetPreparedSummonData(currentPlayerId);

        var playerBoard = board.SelectMutableBoard(currentPlayerId);
        playerBoard[position] = monsterToSummon;

        // Mover reemplazado a la reserva
        if (replaced != null)
        {
            var reserve = board.GetReserveUnitsForPlayer(currentPlayerId);
            reserve.Remove(monsterToSummon);
            reserve.Insert(0, replaced);
        }
        
        // Consumir turno de invocación
        var delta = turnManager.ConsumeSummonTurn();
        var actionView = new CombatActionView(View);
        actionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }
}