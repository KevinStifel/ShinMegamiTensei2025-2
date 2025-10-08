using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public abstract class AffinityViewBase
{
    protected readonly View View;

    protected AffinityViewBase(View view)
    {
        View = view;
    }

    public abstract void ShowAffinityReaction(UnitBase caster, UnitBase target, int damage);
    
    protected void ShowHp(UnitBase unit)
    {
        View.WriteLine($"{unit.Name} termina con HP:{unit.Stats.HP}/{unit.Stats.MaxHP}");
    }
}