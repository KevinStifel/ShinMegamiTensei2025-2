using System.Collections.Generic;
using Shin_Megami_Tensei;

namespace Shin_Megami_Tensei_View;

public sealed class AllySelectorView : TargetSelectorViewBase
{
    public AllySelectorView(View view) : base(view) { }

    public override void ShowAvailableTargets(UnitBase caster, List<UnitBase> allies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {caster.Name}");

        for (int i = 0; i < allies.Count; i++)
        {
            var ally = allies[i];
            View.WriteLine($"{i + 1}-{ally.Name} HP:{ally.Stats.HP}/{ally.Stats.MaxHP} MP:{ally.Stats.MP}/{ally.Stats.MaxMP}");
        }

        View.WriteLine($"{allies.Count + 1}-Cancelar");
    }
}