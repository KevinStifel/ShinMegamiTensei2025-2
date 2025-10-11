using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public abstract class AffinityViewBase
{
    protected readonly View View;
    protected readonly string AttackElementalVerb;

    protected AffinityViewBase(View view, AffinityElement element)
    {
        View = view;
        AttackElementalVerb = ElementMessageHelper.GetElementalMessage(element);
    }

    public abstract void ShowAffinityReaction(UnitBase casterUnit, UnitBase targetUnit, int damage);
    
    public virtual void ShowHp(UnitBase casterUnit, UnitBase targetUnit)
    {
        ShowHp(targetUnit);
    }
    protected void ShowHp(UnitBase unit)
    {
        View.WriteLine($"{unit.Name} termina con HP:{unit.Stats.HP}/{unit.Stats.MaxHP}");
    }
}