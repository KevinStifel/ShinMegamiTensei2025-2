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

    // Revivir
    // Revivir
    public void ShowReviveEffect(UnitBase caster, UnitBase target, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{caster.Name} revive a {target.Name}");
        View.WriteLine($"{target.Name} recibe {healAmount} de HP");
        View.WriteLine($"{target.Name} termina con HP:{target.Stats.HP}/{target.Stats.MaxHP}");
    }


    // Invocación (Summon)
    public void ShowSummon(UnitBase target)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{target.Name} ha sido invocado");
    }

    // Habilidades especiales (Sabbatma, Charge, Concentrate, etc.)
    public void ShowSpecial(UnitBase caster, string skillName)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{caster.Name} usa {skillName}");
    }
}