using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class HealEffect : EffectBase
{
    public HealEffect(AffinityBehavior behavior, View baseView)
        : base(behavior, baseView) { }

    public override void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData)
    {
        int healAmount = (int)(target.Stats.MaxHP * (skillData.Power / 100.0));
        target.Stats.Heal(healAmount);
        EffectView.ShowHeal(target, healAmount);
    }
}