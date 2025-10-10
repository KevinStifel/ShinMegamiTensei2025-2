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

        CombatActionView actionView = new CombatActionView(View);
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        AffinityBehavior? lastBehavior = null;
        UnitBase? lastTarget = null;

        for (int index = 0; index < targets.Count; index++)
        {
            var target = targets[index];
            var element = AffinityMapper.Parse(skillData.Type);
            var reaction = target.Affinity.GetAffinityReaction(element);
            var affinityBehavior = AffinityBehaviorFactory.Create(reaction);
            lastBehavior = affinityBehavior;

            var affinityView = AffinityViewFactory.Create(affinityBehavior.Type, View);

            string verb = ElementMessageHelper.GetElementalMessage(element);
            int damage = DamageCalculator.CalculateFinalDamageForSkill(caster, skillData, affinityBehavior);

            affinityBehavior.ApplyEffect(caster, target, damage);
            affinityView.ShowAffinityReaction(caster, target, damage, verb);

            lastTarget = target;

            if (index == targets.Count - 1)
                affinityView.ShowHp(caster, target);
        }

        if (lastBehavior != null)
        {
            var turnChange = turnManager.ApplyAffinityTurnEffect(lastBehavior);
            actionView.ShowTurnConsumption(turnChange);
        }

        if (lastTarget != null && lastTarget.Stats.HP == 0)
            boardManager.HandleUnitDeath(enemyPlayerId, lastTarget);

        if (caster.Stats.HP <= 0)
            boardManager.HandleUnitDeath(currentPlayerId, caster);
    }
}
