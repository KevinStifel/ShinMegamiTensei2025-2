using System;
using System.Collections.Generic;
using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    public DamageEffect(View view)
        : base(view) { }

    public override void ApplyEffect(
        UnitBase caster,
        List<UnitBase> targets,
        SkillData skillData,
        TurnManager turnManager,
        int currentPlayerId,
        BoardManager board)
    {
        CombatActionView actionView = new CombatActionView(View);
        
        int enemyPlayerId = currentPlayerId == 1 ? 2 : 1;
        AffinityBehavior? lastBehavior = null;
        UnitBase? lastTarget = null;

        for (int i = 0; i < targets.Count; i++)
        {
            var target = targets[i];

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

            if (i == targets.Count - 1)
                affinityView.ShowHp(caster, target);
        }

        if (lastBehavior != null)
        {
            var delta = turnManager.ApplyAffinityTurnEffect(lastBehavior);
            actionView.ShowTurnConsumption(delta.ConsumedFull, delta.ConsumedBlinking, delta.GainedBlinking);
        }
        
        if (lastTarget != null && lastTarget.Stats.HP == 0)
        {
            board.HandleUnitDeath(enemyPlayerId, lastTarget);
        }
        
        if (caster.Stats.HP <= 0)
        {
            board.HandleUnitDeath(currentPlayerId, caster);
        }
    }
}
