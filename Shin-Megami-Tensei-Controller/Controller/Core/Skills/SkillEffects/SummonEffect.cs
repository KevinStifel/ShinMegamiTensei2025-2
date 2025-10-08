using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class SummonEffect : EffectBase
{
    public SummonEffect(AffinityBehavior behavior, View baseView)
        : base(behavior, baseView) { }

    public override void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData)
    {
        EffectView.ShowSummon(caster, target);
        // La lógica de invocación se implementará en entregas futuras
    }
}