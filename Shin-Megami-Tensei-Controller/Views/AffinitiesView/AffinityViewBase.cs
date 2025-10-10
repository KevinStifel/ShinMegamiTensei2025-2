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

    public abstract void ShowAffinityReaction(UnitBase caster, UnitBase target, int damage);
    
    public virtual void ShowHp(UnitBase caster, UnitBase target)
    {
        ShowHp(target);
    }

    // 🔹 Método auxiliar reutilizable
    protected void ShowHp(UnitBase unit)
    {
        View.WriteLine($"{unit.Name} termina con HP:{unit.Stats.HP}/{unit.Stats.MaxHP}");
    }
}