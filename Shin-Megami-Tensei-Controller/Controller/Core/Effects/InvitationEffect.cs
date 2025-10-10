using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class InvitationEffect : EffectBase
{
    public InvitationEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        TurnManager turnManager,
        int currentPlayerId,
        BoardManager boardManager)
    {
        var monsterToSummon = targets[0];
        var (chosenPosition, occupant) = boardManager.GetPreparedSummonData(currentPlayerId);
        var playerBoard = boardManager.SelectMutableBoard(currentPlayerId);
        var reserveUnits = boardManager.GetReserveUnitsForPlayer(currentPlayerId);

        // Colocar al monstruo
        playerBoard[chosenPosition] = monsterToSummon;
        reserveUnits.Remove(monsterToSummon);
        if (occupant != null)
            reserveUnits.Insert(0, occupant);

        // 🧩 Caso 1: estaba muerto → revive e imprime el bloque completo
        if (monsterToSummon.Stats.HP == 0)
        {
            int healAmount = monsterToSummon.Stats.MaxHP;
            monsterToSummon.Stats.Heal(healAmount);
            EffectView.ShowSummonAndReviveEffect(caster, monsterToSummon, healAmount);
        }
        else
        {
            // 🧩 Caso 2: estaba vivo → imprime invocación normal
            EffectView.ShowSummonResult(monsterToSummon);
        }

        // 🔄 Actualizar orden
        turnManager.UpdateOrderAfterSummon(caster, monsterToSummon, occupant);

        // ⏱️ Consumir turno neutral
        var delta = turnManager.ConsumeNeutralTurn();
        new CombatActionView(View).ShowTurnConsumption(
            delta.ConsumedFull,
            delta.ConsumedBlinking,
            delta.GainedBlinking
        );
    }
}