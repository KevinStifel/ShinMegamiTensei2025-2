using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class PhysicalAttackAction : OffensiveActionBase
{
    protected override AffinityElement Element => AffinityElement.Physical;

    public PhysicalAttackAction(View view) : base(view) { }
}