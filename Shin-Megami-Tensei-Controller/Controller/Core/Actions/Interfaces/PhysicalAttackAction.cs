using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

/// <summary>
/// Acción ofensiva física básica.
/// </summary>
public sealed class PhysicalAttackAction : OffensiveActionBase
{
    protected override AffinityElement Element => AffinityElement.Physical;

    public PhysicalAttackAction(View view) : base(view) { }
}