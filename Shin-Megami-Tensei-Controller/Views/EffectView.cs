using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class EffectView : AbstractView
{
    public EffectView(View view) : base(view) { }
    
    // Curación
    public void ShowHealEffect(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{casterUnit.Name} cura a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {healAmount} de HP");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
    }

    public void ShowReviveEffect(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{casterUnit.Name} revive a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {healAmount} de HP");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
    }
    public void ShowSummonResult(UnitBase targetUnit)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{targetUnit.Name} ha sido invocado");
    }
    public void ShowSummonAndReviveEffect(UnitBase casterUnit, UnitBase targetUnit, int healAmount)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{targetUnit.Name} ha sido invocado");
        View.WriteLine($"{casterUnit.Name} revive a {targetUnit.Name}");
        View.WriteLine($"{targetUnit.Name} recibe {healAmount} de HP");
        View.WriteLine($"{targetUnit.Name} termina con HP:{targetUnit.Stats.HP}/{targetUnit.Stats.MaxHP}");
    }
}