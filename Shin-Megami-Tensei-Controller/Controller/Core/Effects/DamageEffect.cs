using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    public DamageEffect(View view) : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
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

        for (int targetIndex = 0; targetIndex < targets.Count; targetIndex++)
        {
            var targetUnit = targets[targetIndex];

            var elementType = AffinityMapper.Parse(skillData.Type);
            var reaction = targetUnit.Affinity.GetAffinityReaction(elementType);
            var affinityBehavior = AffinityBehaviorFactory.Create(reaction);
            lastAffinityBehavior = affinityBehavior;

            ApplyDamageAndShowReaction(caster, targetUnit, skillData, affinityBehavior);

            lastTargetUnit = targetUnit;

            if (IsLastTarget(targetIndex, targets))
                ShowFinalHp(caster, targetUnit, affinityBehavior);
        }

        ApplyTurnChange(turnManager, lastAffinityBehavior, actionView);
        HandleUnitsDeath(boardManager, currentPlayerId, enemyPlayerId, caster, lastTargetUnit);
    }

    private static bool IsLastTarget(int targetIndex, IReadOnlyList<UnitBase> targets)
        => targetIndex == targets.Count - 1;

    private void ApplyDamageAndShowReaction(UnitBase caster, UnitBase targetUnit, SkillData skillData, AffinityBehavior affinityBehavior)
    {
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View);
        var elementType = AffinityMapper.Parse(skillData.Type);
        string attackVerb = ElementMessageHelper.GetElementalMessage(elementType);

        int inflictedDamage = DamageCalculator.CalculateFinalDamageForSkill(caster, skillData, affinityBehavior);

        affinityBehavior.ApplyEffect(caster, targetUnit, inflictedDamage);
        affinityView.ShowAffinityReaction(caster, targetUnit, inflictedDamage, attackVerb);
    }

    private void ShowFinalHp(UnitBase caster, UnitBase targetUnit, AffinityBehavior affinityBehavior)
    {
        var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View);
        affinityView.ShowHp(caster, targetUnit);
    }

    private static void ApplyTurnChange(TurnManager turnManager, AffinityBehavior? lastAffinityBehavior, CombatActionView actionView)
    {
        if (lastAffinityBehavior == null)
            return;

        var turnChange = turnManager.ApplyAffinityTurnEffect(lastAffinityBehavior);
        actionView.ShowTurnConsumption(turnChange);
    }

    private static void HandleUnitsDeath(BoardManager boardManager, int currentPlayerId, int enemyPlayerId, UnitBase caster, UnitBase? lastTargetUnit)
    {
        bool isTargetDead = lastTargetUnit is { Stats.HP: 0 };
        bool isCasterDead = caster.Stats.HP <= 0;

        if (isTargetDead)
            boardManager.HandleUnitDeath(enemyPlayerId, lastTargetUnit!);

        if (isCasterDead)
            boardManager.HandleUnitDeath(currentPlayerId, caster);
    }
}
