using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class EffectView : AbstractView
{
    public EffectView(View view) : base(view) { }
    
    // Curación
    public void ShowHealEffect(UnitBase caster, UnitBase target, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{caster.Name} cura a {target.Name}");
        View.WriteLine($"{target.Name} recibe {healAmount} de HP");
        View.WriteLine($"{target.Name} termina con HP:{target.Stats.HP}/{target.Stats.MaxHP}");
    }

    public void ShowReviveEffect(UnitBase caster, UnitBase target, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{caster.Name} revive a {target.Name}");
        View.WriteLine($"{target.Name} recibe {healAmount} de HP");
        View.WriteLine($"{target.Name} termina con HP:{target.Stats.HP}/{target.Stats.MaxHP}");
    }
    public void ShowSummonResult(UnitBase target)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{target.Name} ha sido invocado");
    }
    public void ShowSummonAndReviveEffect(UnitBase caster, UnitBase target, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{target.Name} ha sido invocado");
        View.WriteLine($"{caster.Name} revive a {target.Name}");
        View.WriteLine($"{target.Name} recibe {healAmount} de HP");
        View.WriteLine($"{target.Name} termina con HP:{target.Stats.HP}/{target.Stats.MaxHP}");
    }
}