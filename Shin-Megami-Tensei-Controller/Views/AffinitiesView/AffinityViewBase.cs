using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public abstract class AffinityViewBase
{
    protected readonly View View;

    protected AffinityViewBase(View view)
    {
        View = view;
    }

    public abstract void ShowAffinityReaction(UnitBase caster, UnitBase target, int damage, string verb);
    
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