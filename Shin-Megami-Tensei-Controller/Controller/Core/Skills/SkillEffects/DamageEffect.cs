using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageEffect : EffectBase
{
    private readonly AffinityViewBase _affinityView;

    public DamageEffect(AffinityBehavior behavior, View view)
        : base(behavior, view)
    {
        _affinityView = AffinityViewFactory.Create(behavior.Type, view);
    }

    public override void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData)
    {
        
        var element = AffinityMapper.Parse(skillData.Type);
        string verb = ElementMessageHelper.GetElementalMessage(element);

        int damage = DamageCalculator.CalculateFinalDamageForSkill(caster, skillData, Behavior);

        Behavior.ApplyEffect(caster, target, damage);
        _affinityView.ShowAffinityReaction(caster, target, damage, verb);
        
        if (IsLastHit())
            _affinityView.ShowHp(caster, target);

        DecrementHit();
    }
}