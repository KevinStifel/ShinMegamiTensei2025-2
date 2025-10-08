using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class NeutralAffinityView : AffinityViewBase
{
    public NeutralAffinityView(View view) : base(view) { }

    public override void ShowAffinityReaction(UnitBase caster, UnitBase target, int damage)
    {
        View.WriteLine($"{target.Name} recibe {damage} de daño");
        ShowHp(target);
    }
}