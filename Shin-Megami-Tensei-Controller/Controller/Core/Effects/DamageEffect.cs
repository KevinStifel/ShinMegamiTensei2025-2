using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    public DamageEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase casterUnit,
        List<UnitBase> targetUnits,
        SkillData skillData,
        BattleFlowContext battleFlowContext)
    {
        var turnManager = battleFlowContext.TurnManager;
        var boardManager = battleFlowContext.BoardManager;
        var currentPlayerId = battleFlowContext.CurrentPlayerId;
        var actionView = new CombatActionView(View);

        int enemyPlayerId = BattleHelper.GetEnemyPlayerId(currentPlayerId);

        AffinityBehavior? lastAffinityBehavior = null;
        UnitBase? lastTargetUnit = null;

        for (int targetIndex = 0; targetIndex < targetUnits.Count; targetIndex++)
        {
            var targetUnit = targetUnits[targetIndex];

            var elementType = AffinityMapper.Parse(skillData.Type);
            var affinityReaction = targetUnit.Affinity.GetAffinityReaction(elementType);
            var affinityBehavior = AffinityBehaviorFactory.Create(affinityReaction);
            var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View, elementType);

            lastAffinityBehavior = affinityBehavior;
            lastTargetUnit = targetUnit;

            int inflictedDamage = CalculateDamage(casterUnit, skillData, affinityBehavior);
            affinityBehavior.ApplyEffect(casterUnit, targetUnit, inflictedDamage);
            affinityView.ShowAffinityReaction(casterUnit, targetUnit, inflictedDamage);

            bool isLastTargetInAttack = targetIndex == targetUnits.Count - 1;
            if (isLastTargetInAttack)
                affinityView.ShowHp(casterUnit, targetUnit);
        }

        ApplyTurnChange(turnManager, lastAffinityBehavior, actionView);

        bool targetHasDied = lastTargetUnit is { Stats.HP: 0 };
        bool casterHasDied = casterUnit.Stats.HP <= 0;

        if (targetHasDied)
            HandleUnitDeath(boardManager, enemyPlayerId, lastTargetUnit!);

        if (casterHasDied)
            HandleUnitDeath(boardManager, currentPlayerId, casterUnit);
    }

    private static int CalculateDamage(UnitBase casterUnit, SkillData skillData, AffinityBehavior affinityBehavior)
    {
        return DamageCalculator.CalculateFinalDamageForSkill(casterUnit, skillData, affinityBehavior);
    }
    
    private static void ApplyTurnChange(TurnManager turnManager, AffinityBehavior? lastAffinityBehavior, CombatActionView actionView)
    {
        if (lastAffinityBehavior == null)
            return;

        var turnChange = turnManager.ApplyAffinityTurnEffect(lastAffinityBehavior);
        actionView.ShowTurnConsumption(turnChange);
    }

    private static void HandleUnitDeath(BoardManager boardManager, int playerId, UnitBase unit)
    {
        boardManager.HandleUnitDeath(playerId, unit);
    }
}
