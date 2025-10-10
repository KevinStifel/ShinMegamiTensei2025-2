using System.Linq;
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

        foreach (var (targetUnit, index) in targetUnits.Select((unit, i) => (unit, i)))
        {
            var elementType = AffinityMapper.Parse(skillData.Type);
            var affinityReaction = targetUnit.Affinity.GetAffinityReaction(elementType);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);
            var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, elementType);

            int inflictedDamage = DamageCalculator.CalculateFinalDamageForSkill(casterUnit, skillData, affinityBehavior);
            affinityBehavior.ApplyEffect(casterUnit, targetUnit, inflictedDamage);
            affinityView.ShowAffinityReaction(casterUnit, targetUnit, inflictedDamage);

            bool isLastTarget = (index == targetUnits.Count - 1);
            if (isLastTarget)
                affinityView.ShowHp(casterUnit, targetUnit);
        }

        var lastTarget = targetUnits.Last();
        var lastAffinityBehavior = GetLastAffinityBehavior(lastTarget, skillData);

        ApplyTurnChange(turnManager, lastAffinityBehavior);
        HandleDeaths(boardManager, currentPlayerId, enemyPlayerId, casterUnit, lastTarget);
    }

    private static AffinityBehavior GetLastAffinityBehavior(UnitBase targetUnit, SkillData skillData)
    {
        var elementType = AffinityMapper.Parse(skillData.Type);
        var affinityReaction = targetUnit.Affinity.GetAffinityReaction(elementType);
        return AffinityBehaviorFactory.Create(affinityReaction);
    }

    private void ApplyTurnChange(TurnManager turnManager, AffinityBehavior affinityBehavior)
    {
        var turnChange = turnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }

    private static void HandleDeaths(BoardManager boardManager, int currentPlayerId, int enemyPlayerId, UnitBase caster, UnitBase lastTarget)
    {
        bool isTargetDead = lastTarget.Stats.HP == 0;
        bool isCasterDead = caster.Stats.HP <= 0;

        if (isTargetDead)
            boardManager.HandleUnitDeath(enemyPlayerId, lastTarget);

        if (isCasterDead)
            boardManager.HandleUnitDeath(currentPlayerId, caster);
    }
}
