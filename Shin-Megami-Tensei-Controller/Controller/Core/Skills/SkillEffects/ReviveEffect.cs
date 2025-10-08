using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class ReviveEffect : EffectBase
{
    public ReviveEffect(AffinityBehavior behavior, View baseView)
        : base(behavior, baseView) { }

    public override void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData)
    {
        // Consideramos "muerto" si HP = 0
        bool isDead = target.Stats.HP <= 0;
        if (!isDead) return;

        int healAmount = (int)(target.Stats.MaxHP * (skillData.Power / 100.0));
        target.Stats.Heal(healAmount);
        EffectView.ShowRevive(target);
    }
}