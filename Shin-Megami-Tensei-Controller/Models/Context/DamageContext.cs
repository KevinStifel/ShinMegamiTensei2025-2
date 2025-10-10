using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageContext
{
    public UnitBase Caster { get; }
    public UnitBase Target { get; }
    public AffinityBehavior AffinityBehavior { get; }
    public AffinityViewBase AffinityView { get; }

    public DamageContext(
        UnitBase caster,
        UnitBase target,
        AffinityBehavior affinityBehavior,
        AffinityViewBase affinityView
        )
    {
        Caster = caster;
        Target = target;
        AffinityBehavior = affinityBehavior;
        AffinityView = affinityView;
    }
}