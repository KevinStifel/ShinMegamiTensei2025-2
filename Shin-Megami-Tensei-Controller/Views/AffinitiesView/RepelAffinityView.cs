using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class RepelAffinityView : AffinityViewBase
{
    public RepelAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage)
    {
        View.WriteLine($"{casterUnit.Name} {AttackElementalVerb} a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} devuelve {damage} daño a {casterUnit.Name}");
    }

    public override void ShowHp(UnitBase attackerUnit, UnitBase targetUnit)
    {
        ShowHp(attackerUnit);
    }
}