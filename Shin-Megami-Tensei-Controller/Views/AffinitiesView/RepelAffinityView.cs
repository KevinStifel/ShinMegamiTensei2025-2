using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class RepelAffinityView : AffinityViewBase
{
    public RepelAffinityView(View view, AffinityElement element) : base(view, element) { }

    public override void ShowAffinityReaction(UnitBase attackerUnit, UnitBase targetEnemyUnit, int damage)
    {
        View.WriteLine($"{attackerUnit.Name} {AttackElementalVerb} a {targetEnemyUnit.Name}");
        View.WriteLine($"{targetEnemyUnit.Name} devuelve {damage} daño a {attackerUnit.Name}");
    }

    public override void ShowHp(UnitBase attackerUnit, UnitBase target)
    {
        ShowHp(attackerUnit);
    }
}