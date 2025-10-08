using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public abstract class EffectBase
{
    protected readonly AffinityBehavior Behavior;
    protected readonly EffectView EffectView;
    protected readonly View View;

    protected EffectBase(AffinityBehavior behavior, View view)
    {
        Behavior = behavior;
        EffectView = new EffectView(view);
        View = view;
    }

    public abstract void ApplyEffect(UnitBase caster, UnitBase target, SkillData skillData);
}