using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class DamageContext
{
    public UnitBase AttackerUnit { get; }
    public UnitBase TargetUnit { get; }
    public AffinityBehavior AffinityBehavior { get; }
    public AffinityViewBase AffinityView { get; }

    public DamageContext(
        UnitBase attackerUnit,
        UnitBase targetUnit,
        AffinityBehavior affinityBehavior,
        AffinityViewBase affinityView
        )
    {
        AttackerUnit = attackerUnit;
        TargetUnit = targetUnit;
        AffinityBehavior = affinityBehavior;
        AffinityView = affinityView;
    }
}