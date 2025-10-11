using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    public DamageEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targetEnemyUnits,
        SkillExecutionContext skillExecutionContext)
    {
        var turnManager = skillExecutionContext.TurnManager;
        var boardManager = skillExecutionContext.BoardManager;
        var currentPlayerId = skillExecutionContext.CurrentPlayerId;
        var skillData = skillExecutionContext.SkillData;

        int enemyPlayerId = BattleHelper.GetEnemyPlayerId(currentPlayerId);

        foreach (var (targetEnemyUnit, index) in targetEnemyUnits.Select((unit, index) => (unit, index)))
        {
            var elementType = AffinityMapper.Parse(skillData.Type);
            var affinityReaction = targetEnemyUnit.Affinity.GetAffinityReaction(elementType);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);
            var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, elementType);

            int inflictedDamage = DamageCalculator.CalculateFinalDamageForSkill(casterUnit, skillData, affinityBehavior);
            affinityBehavior.ApplyEffect(casterUnit, targetEnemyUnit, inflictedDamage);
            affinityView.ShowAffinityReaction(casterUnit, targetEnemyUnit, inflictedDamage);

            bool isLastTarget = IsLastTarget(index, targetEnemyUnits.Count);
            if (isLastTarget)
                affinityView.ShowHp(casterUnit, targetEnemyUnit);
        }

        var lastTarget = targetEnemyUnits.Last();
        var lastAffinityBehavior = GetLastAffinityBehavior(lastTarget, skillData);

        ApplyTurnChange(turnManager, lastAffinityBehavior);
        
        HandleDeath(boardManager, enemyPlayerId, lastTarget);
        HandleDeath(boardManager, currentPlayerId, casterUnit);
    }

    private static AffinityBehavior GetLastAffinityBehavior(UnitBase targetEnemyUnit, SkillData skillData)
    {
        var elementType = AffinityMapper.Parse(skillData.Type);
        var affinityReaction = targetEnemyUnit.Affinity.GetAffinityReaction(elementType);
        return AffinityBehaviorFactory.Create(affinityReaction);
    }

    private void ApplyTurnChange(TurnManager turnManager, AffinityBehavior affinityBehavior)
    {
        var turnChange = turnManager.ApplyAffinityTurnEffect(affinityBehavior);
        ActionView.ShowTurnConsumption(turnChange);
    }
    private static void HandleDeath(BoardManager boardManager, int playerId, UnitBase unit)
    {
        if (unit.Stats.HP == 0)
            boardManager.HandleUnitDeath(playerId, unit);
    }
    private static bool IsLastTarget(int currentIndex, int totalTargets)
    {
        return currentIndex == totalTargets - 1;
    }
}
