using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    public DamageEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targetUnits,
        SkillExecutionContext skillContext)
    {
        var turnManager = skillContext.TurnManager;
        var boardManager = skillContext.BoardManager;
        var currentPlayerId = skillContext.CurrentPlayerId;
        var skillData = skillContext.SkillData;

        int enemyPlayerId = BattleHelper.GetEnemyPlayerId(currentPlayerId);

        AffinityBehavior? lastAffinityBehavior = null;
        UnitBase? lastTargetUnit = null;

        for (int index = 0; index < targetUnits.Count; index++)
        {
            var targetUnit = targetUnits[index];
            var elementType = AffinityMapper.Parse(skillData.Type);
            var affinityReaction = targetUnit.Affinity.GetAffinityReaction(elementType);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);
            var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, elementType);

            lastAffinityBehavior = affinityBehavior;
            lastTargetUnit = targetUnit;

            int inflictedDamage = DamageCalculator.CalculateFinalDamageForSkill(casterUnit, skillData, affinityBehavior);
            affinityBehavior.ApplyEffect(casterUnit, targetUnit, inflictedDamage);
            affinityView.ShowAffinityReaction(casterUnit, targetUnit, inflictedDamage);

            bool isLastTarget = index == targetUnits.Count - 1;
            if (isLastTarget)
                affinityView.ShowHp(casterUnit, targetUnit);
        }

        if (lastAffinityBehavior != null)
        {
            var turnChange = turnManager.ApplyAffinityTurnEffect(lastAffinityBehavior);
            ActionView.ShowTurnConsumption(turnChange);
        }

        bool isTargetDead = lastTargetUnit is { Stats.HP: 0 };
        bool isCasterDead = casterUnit.Stats.HP <= 0;

        if (isTargetDead)
            boardManager.HandleUnitDeath(enemyPlayerId, lastTargetUnit!);

        if (isCasterDead)
            boardManager.HandleUnitDeath(currentPlayerId, casterUnit);
    }
}
