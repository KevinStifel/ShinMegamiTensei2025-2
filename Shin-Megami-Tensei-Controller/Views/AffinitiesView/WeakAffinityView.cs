using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class WeakAffinityView : AffinityViewBase
{
    public WeakAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        View.WriteLine($"{casterUnit.Name} {AttackElementalVerb} a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} es débil contra el ataque de {casterUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {damage} de daño");
    }
}