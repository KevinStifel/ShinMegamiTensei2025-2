using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class ResistAffinityView : AffinityViewBase
{
    public ResistAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase caster, UnitBase target, int damage)
    {
        View.WriteLine($"{caster.Name} {AttackElementalVerb} a {target.Name}");
        View.WriteLine($"{target.Name} es resistente el ataque de {caster.Name}");
        View.WriteLine($"{target.Name} recibe {damage} de daño");
    }
}