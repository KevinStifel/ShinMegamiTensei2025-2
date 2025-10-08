using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    public DamageEffect(AffinityBehavior behavior, View view)
        : base(behavior, view) { }

    public override void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData)
    {
        int damage = DamageCalculator.CalculateFinalDamageForSkill(caster, skillData, Behavior);
        var affinityView = AffinityViewFactory.Create(Behavior.Type, View);

        // Aplicar efecto lógico (si existe en Behavior)
        Behavior.ApplyEffect(caster, target, damage);
        
        affinityView.ShowAffinityReaction(caster, target, damage);
    }
}