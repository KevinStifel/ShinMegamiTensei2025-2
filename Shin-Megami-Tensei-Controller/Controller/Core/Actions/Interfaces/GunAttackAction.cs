using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public sealed class GunAttackAction : OffensiveActionBase
{
    protected override AffinityElement Element => AffinityElement.Gun;

    public GunAttackAction(View view) : base(view) { }
}