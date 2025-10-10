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
        var summonEffect = new SummonEffect(View);
        var (chosenPosition, occupant) = board.GetPreparedSummonData(currentPlayerId);
        var playerBoard = board.SelectMutableBoard(currentPlayerId);
        var reserveUnits = board.GetReserveUnitsForPlayer(currentPlayerId);
        var monsterToSummon = targets[0];

        // 🧩 Siempre invoca como “Samurai”
        summonEffect.ApplySamuraiSummon(monsterToSummon, chosenPosition, occupant, playerBoard, reserveUnits);

        turnManager.UpdateOrderAfterSummon(caster, monsterToSummon, occupant);

        // ⏱️ Consumir turno neutro
        var delta = turnManager.ConsumeNeutralTurn();
        new CombatActionView(View).ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }
}