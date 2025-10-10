using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class WeakAffinityView : AffinityViewBase
{
    public WeakAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase caster, UnitBase target, int damage)
    {
        View.WriteLine($"{caster.Name} {AttackElementalVerb} a {target.Name}");
        View.WriteLine($"{target.Name} es débil contra el ataque de {caster.Name}");
        View.WriteLine($"{target.Name} recibe {damage} de daño");
    }
}