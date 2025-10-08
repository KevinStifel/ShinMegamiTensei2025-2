using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SpecialEffect : EffectBase
{
    public SpecialEffect(AffinityBehavior behavior, View baseView)
        : base(behavior, baseView) { }

    public override void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData)
    {
        EffectView.ShowSpecial(caster, skillData.Name);
    }
}