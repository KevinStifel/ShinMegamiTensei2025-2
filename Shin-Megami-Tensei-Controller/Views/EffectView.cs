using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class EffectView : AbstractView
{
    public EffectView(View view) : base(view) { }
    
    // Curación
    public void ShowHeal(UnitBase target, int healAmount)
    {
        View.WriteLine($"{target.Name} recupera {healAmount} de HP");
    }

    // Revivir
    public void ShowRevive(UnitBase target)
    {
        View.WriteLine($"{target.Name} ha sido revivido");
    }

    // Invocación (Summon)
    public void ShowSummon(UnitBase caster, UnitBase target)
    {
        View.WriteLine($"{caster.Name} invoca a {target.Name}");
    }

    // Habilidades especiales (Sabbatma, Charge, Concentrate, etc.)
    public void ShowSpecial(UnitBase caster, string skillName)
    {
        View.WriteLine($"{caster.Name} usa {skillName}");
    }
}