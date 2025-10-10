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
        BoardManager boardManager)
    {
        var summonEffect = new SummonEffect(View);
        var (chosenPosition, occupant) = boardManager.GetPreparedSummonData(currentPlayerId);
        var playerBoard = boardManager.SelectMutableBoard(currentPlayerId);
        var reserveUnits = boardManager.GetReserveUnitsForPlayer(currentPlayerId);
        var monsterToSummon = targets[0];

        summonEffect.ApplySamuraiSummon(monsterToSummon, chosenPosition, occupant, playerBoard, reserveUnits);

        turnManager.UpdateOrderAfterSummon(caster, monsterToSummon, occupant);

        var delta = turnManager.ConsumeNeutralTurn();
        new CombatActionView(View).ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
    }
}